using Student_Management_System.Service.DTO.GetDTO;
using Studnet_Management_System.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Student_Management_System.Service.DTO.AddDTO;
using Student_Management_System.Service.DTO.UpdateDTO;

namespace Student_Management_System.Service.AuoMapperProfile
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Student
            CreateMap<Student,GetStudentDTO>();
            CreateMap<AddStudentDTO, Student>();
            CreateMap<UpdateStudentDTO, Student>();
            #endregion

            #region User
            CreateMap<User, GetUserDTO>();
            CreateMap<AddUserDTO, User>();
            CreateMap<UpdateUserDTO, User>();
            #endregion
        }
    }
}
