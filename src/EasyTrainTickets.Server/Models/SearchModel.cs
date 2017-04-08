using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyTrainTickets.Common.DTOs;
using EasyTrainTickets.Domain.Model;
using AutoMapper;
using EasyTrainTickets.Domain.Services;
using EasyTrainTickets.Domain.Data;
using System.Threading.Tasks;

namespace EasyTrainTickets.Server.Models
{
    public class SearchModel
    {
        public List<ConnectionPathDTO> Search(FilterPathsDTO fp, IUnitOfWorkFactory unitOfWorkFactory)
        {
            List<Path> paths = Mapper.Map<List<PathDTO>, List<Path>>(fp.Paths);
            List<ConnectionPath> conPaths = new List<ConnectionPath>();
            IEasyTrainTicketsDbEntities[] dbContexts = new IEasyTrainTicketsDbEntities[paths.Count];
            Parallel.For(0, paths.Count, i =>
            {
                dbContexts[i] = unitOfWorkFactory.CreateUnitOfWork();

                List<ConnectionPath> candidatePaths = paths[i].SecondSearch(fp.UserTime, dbContexts[i]);
                foreach (var conpath in candidatePaths)
                {
                    conPaths.Add(conpath);
                }

            });

            JourneyTimeFilter(ref conPaths);

            StartTimeFilter(ref conPaths);

            EndTimeFilter(ref conPaths);

            return Mapper.Map<List<ConnectionPath>, List<ConnectionPathDTO>>(conPaths);

        }

        private void JourneyTimeFilter(ref List<ConnectionPath> conPaths)
        {
            if (conPaths.Count == 0) return;
            int best = conPaths.Min(cp => cp.JourneyTime);
            for (int i = 0; i < conPaths.Count; i++)
            {
                if (conPaths[i].JourneyTime > 2.5 * best)
                {
                    conPaths.RemoveAt(i);
                    i--;
                }
            }
        }

        private void StartTimeFilter(ref List<ConnectionPath> conPaths)
        {
            if (conPaths.Count == 0) return;
            var startTimes = conPaths.Select(cp => cp.ConnectionsParts.First().StartTime).Distinct().ToList();
            foreach (var startTime in startTimes)
            {
                var tracks = conPaths.Where(cp => DateTime.Compare(cp.ConnectionsParts[0].StartTime, startTime) == 0).ToList();
                int minChanges = tracks.Min(cp => cp.Changes);
                int bestTime = tracks.Min(cp => cp.JourneyTime);

                var bestTracks = tracks.FindAll(cp => cp.JourneyTime == bestTime).ToList();
                int minBestChanges = bestTracks.Min(cp => cp.Changes);
                var bestTrack = bestTracks.Find(cp => cp.Changes == minBestChanges);

                foreach (var path in tracks)
                {
                    if ((path.JourneyTime > bestTime + 120 && path.Changes > 0) || path.JourneyTime > bestTime && path.Changes >= minBestChanges)
                        conPaths.Remove(path);
                }
            }
        }

        private void EndTimeFilter(ref List<ConnectionPath> conPaths)
        {
            if (conPaths.Count == 0) return;
            var endTimes = conPaths.Select(cp => cp.ConnectionsParts.Last().EndTime).Distinct().ToList();
            foreach (var endTime in endTimes)
            {
                var tracks = conPaths.Where(cp => DateTime.Compare(cp.ConnectionsParts.Last().EndTime, endTime) == 0).ToList();
                int minChanges = tracks.Min(cp => cp.Changes);
                int bestTime = tracks.Min(cp => cp.JourneyTime);

                var bestTracks = tracks.FindAll(cp => cp.JourneyTime == bestTime).ToList();
                int minBestChanges = bestTracks.Min(cp => cp.Changes);
                var bestTrack = bestTracks.Find(cp => cp.Changes == minBestChanges);
                foreach (var path in tracks)
                {
                    if ((path.JourneyTime > bestTime + 120 && path.Changes > 0) || path.JourneyTime>bestTime && path.Changes>=minBestChanges)
                        conPaths.Remove(path);
                }
            }
        }
    }
}