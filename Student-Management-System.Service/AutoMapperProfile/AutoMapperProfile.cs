using AutoMapper;
using Student_Management_System.Service.DTO.AddDTO;
using Student_Management_System.Service.DTO.GetDTO;
using Student_Management_System.Service.DTO.UpdateDTO;
using Studnet_Management_System.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System.Service.AutoMapperProfile
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile() {

            #region Admin
            CreateMap<Admin, GetAdminDTO>();
            CreateMap<AddAdminDTO, Admin>();
            CreateMap<UpdateAdminDTO, Admin>();
            #endregion

            #region Student
            CreateMap<Student, GetStudentDTO>();
            CreateMap<AddStudentDTO, Student>();
            CreateMap<UpdateStudentDTO, Student>();
            #endregion

            #region Teacher
            CreateMap<Teacher, GetTeacherDTO>();
            CreateMap<AddTeacherDTO, Teacher>();
            CreateMap<UpdateTeacherDTO, Teacher>();
            #endregion

            #region Attendence
            CreateMap<Attendence, GetAttendenceDTO>();
            CreateMap<AddAttendenceDTO, Attendence>();
            CreateMap<UpdateAttendenceDTO, Attendence>();
            #endregion

            #region GradeBook
            CreateMap<GradeBook, GetGradeBookDTO>();
            CreateMap<AddGradeBookDTO, GradeBook>();
            CreateMap<UpdateGradeBookDTO, GradeBook>();
            #endregion

            #region User
            CreateMap<User, GetUserDTO>();
            CreateMap<AddUserDTO, User>();
            CreateMap<UpdateUserDTO, User>();
            #endregion
        }
    }
}
