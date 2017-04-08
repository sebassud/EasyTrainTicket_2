using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using EasyTrainTickets.Domain.Model;
using EasyTrainTickets.Common.DTOs;
using EasyTrainTickets.Domain.Services;

namespace EasyTrainTickets.Server.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entity, EntityDTO>();
                cfg.CreateMap<EntityDTO, Entity>();
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, User>();
                cfg.CreateMap<Route, RouteDTO>();
                cfg.CreateMap<RouteDTO, Route>();
                cfg.CreateMap<Train, TrainDTO>();
                cfg.CreateMap<TrainDTO, Train>();
                cfg.CreateMap<PathDTO, Path>();
                cfg.CreateMap<ConnectionPath, ConnectionPathDTO>();
                cfg.CreateMap<ConnectionPathDTO, ConnectionPath>();
                cfg.CreateMap<ConnectionPart, ConnectionPartDTO>().ConvertUsing(con => MappConnectionPart2ConnectionPartDTO(con));
                cfg.CreateMap<ConnectionPartDTO, ConnectionPart>().ConvertUsing(conDTO => MappConnectionPartDTO2ConnectionPart(conDTO));
                cfg.CreateMap<Connection, ConnectionDTO>().ConvertUsing(con => MappConnection2ConnectionDTO(con));
                cfg.CreateMap<ConnectionDTO, Connection>().ConvertUsing(conDTO => MappConnectionDTO2Connection(conDTO));
                cfg.CreateMap<Ticket, TicketDTO>();
                cfg.CreateMap<TicketDTO, Ticket>();
                cfg.CreateMap<ChangePasswordDTO, User>();
                cfg.CreateMap<Discount, DiscountDTO>();
                cfg.CreateMap<DiscountDTO, Discount>();
            });
        }

        private static ConnectionDTO MappConnection2ConnectionDTO(Connection con)
        {
            ConnectionDTO conDTO = new ConnectionDTO()
            {
                EndPlace = con.EndPlace,
                Id = con.Id,
                Version = con.Version,
                Name = con.Name,
                StartPlace = con.StartPlace,
                Train = Mapper.Map<Train, TrainDTO>(con.Train),
                Parts = new List<ConnectionPartDTO>()
            };

            foreach (var part in con.Parts)
            {
                ConnectionPartDTO conpart = new ConnectionPartDTO()
                {
                    EndTime = part.EndTime,
                    FreeSeats = part.FreeSeats,
                    Id = part.Id,
                    Seats = part.Seats,
                    StartTime = part.StartTime,
                    Version = part.Version,
                    Route = Mapper.Map<Route, RouteDTO>(part.Route),
                };
                conpart.Connection = conDTO;
                conDTO.Parts.Add(conpart);
            }
            return conDTO;
        }

        private static Connection MappConnectionDTO2Connection(ConnectionDTO conDTO)
        {
            Connection con = new Connection()
            {
                EndPlace = conDTO.EndPlace,
                Id = conDTO.Id,
                Version = conDTO.Version,
                Name = conDTO.Name,
                StartPlace = conDTO.StartPlace,
                Train = Mapper.Map<TrainDTO, Train>(conDTO.Train),
                Parts = new List<ConnectionPart>()
            };

            foreach (var part in conDTO.Parts)
            {
                ConnectionPart conpart = new ConnectionPart()
                {
                    EndTime = part.EndTime,
                    FreeSeats = part.FreeSeats,
                    Id = part.Id,
                    Seats = part.Seats,
                    StartTime = part.StartTime,
                    Version = part.Version,
                    Route = Mapper.Map<RouteDTO, Route>(part.Route),
                };
                conpart.Connection = con;
                con.Parts.Add(conpart);
            }
            return con;
        }

        private static ConnectionPartDTO MappConnectionPart2ConnectionPartDTO(ConnectionPart conPart)
        {
            ConnectionPartDTO conDTO = new ConnectionPartDTO()
            {
                EndTime = conPart.EndTime,
                FreeSeats = conPart.FreeSeats,
                Id = conPart.Id,
                Seats = conPart.Seats,
                StartTime = conPart.StartTime,
                Version = conPart.Version,
                Route = Mapper.Map<Route, RouteDTO>(conPart.Route),
            };

            conDTO.Connection = new ConnectionDTO()
            {
                EndPlace = conPart.Connection.EndPlace,
                Id = conPart.Connection.Id,
                Version = conPart.Connection.Version,
                Name = conPart.Connection.Name,
                StartPlace = conPart.Connection.StartPlace,
                Train = Mapper.Map<Train, TrainDTO>(conPart.Connection.Train),
            };

            conDTO.Connection.Parts = new List<ConnectionPartDTO>();

            foreach (var part in conPart.Connection.Parts)
            {
                if (part.Id == conDTO.Id) conDTO.Connection.Parts.Add(conDTO);
                else
                {
                    ConnectionPartDTO conpart = new ConnectionPartDTO()
                    {
                        EndTime = part.EndTime,
                        FreeSeats = part.FreeSeats,
                        Id = part.Id,
                        Seats = part.Seats,
                        StartTime = part.StartTime,
                        Version = part.Version,
                        Route = Mapper.Map<Route, RouteDTO>(part.Route),
                    };
                    conpart.Connection = conDTO.Connection;
                    conDTO.Connection.Parts.Add(conpart);
                }
            }

            return conDTO;
        }

        private static ConnectionPart MappConnectionPartDTO2ConnectionPart(ConnectionPartDTO conPartDTO)
        {
            ConnectionPart conPart = new ConnectionPart()
            {
                EndTime = conPartDTO.EndTime,
                FreeSeats = conPartDTO.FreeSeats,
                Id = conPartDTO.Id,
                Seats = conPartDTO.Seats,
                StartTime = conPartDTO.StartTime,
                Version = conPartDTO.Version,
                Route = Mapper.Map<RouteDTO, Route>(conPartDTO.Route),
            };

            conPart.Connection = new Connection()
            {
                EndPlace = conPartDTO.Connection.EndPlace,
                Id = conPartDTO.Connection.Id,
                Version = conPartDTO.Connection.Version,
                Name = conPartDTO.Connection.Name,
                StartPlace = conPartDTO.Connection.StartPlace,
                Train = Mapper.Map<TrainDTO, Train>(conPartDTO.Connection.Train),
            };

            conPart.Connection.Parts = new List<ConnectionPart>();

            foreach (var part in conPartDTO.Connection.Parts)
            {
                if (part.Id == conPart.Id) conPart.Connection.Parts.Add(conPart);
                else
                {
                    ConnectionPart conpart = new ConnectionPart()
                    {
                        EndTime = part.EndTime,
                        FreeSeats = part.FreeSeats,
                        Id = part.Id,
                        Seats = part.Seats,
                        StartTime = part.StartTime,
                        Version = part.Version,
                        Route = Mapper.Map<RouteDTO, Route>(part.Route),
                    };
                    conpart.Connection = conPart.Connection;
                    conPart.Connection.Parts.Add(conpart);
                }
            }

            return conPart;
        }
    }
}