using EasyTrainTickets.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyTrainTickets.Domain.Data;
using EasyTrainTickets.Common.DTOs;
using System.Threading.Tasks;
using AutoMapper;

namespace EasyTrainTickets.Server.Models
{
    public class UserModel
    {
        public UserDTO SignIn(UserDTO userDTO, IEasyTrainTicketsDbEntities dbcontext)
        {
            var record = dbcontext.Users.Where(u => u.Login == userDTO.Login);
            if (record.Count() == 1)
            {
                string sqlpass = record.First().Password;
                if (sqlpass == userDTO.Password)
                {
                    userDTO = AutoMapper.Mapper.Map<User, UserDTO>(record.ToList()[0]);
                    return userDTO;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public UserDTO Registration(UserDTO userDTO, IEasyTrainTicketsDbEntities dbcontext)
        {
            var record = dbcontext.Users.Where((u) => u.Login == userDTO.Login);
            User user = Mapper.Map<UserDTO, User>(userDTO);
            if (record.Count() == 1)
            {
                return null;
            }

            dbcontext.Users.Add(user);
            dbcontext.SaveChanges();
            userDTO = Mapper.Map<User, UserDTO>(user);
            return userDTO;
        }

        public UserDTO ChangePassword(ChangePasswordDTO changePasswordDTO, IEasyTrainTicketsDbEntities dbcontext)
        {
            User user = Mapper.Map<ChangePasswordDTO, User>(changePasswordDTO);

            var record = dbcontext.Users.Where((u) => u.Login == changePasswordDTO.Login);
            if (record.Count() == 0 || record.ToList()[0].Password != user.Password)
            {
                return null;
            }

            record.ToList()[0].Password = changePasswordDTO.NewPassword;

            return Mapper.Map<User, UserDTO>(user);
        }
    }
}