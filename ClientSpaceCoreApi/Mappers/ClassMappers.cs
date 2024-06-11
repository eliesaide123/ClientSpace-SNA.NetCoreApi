using AutoMapper;
using Entities;
using System.Data;

namespace ClientSpaceCoreApi.Mappers
{
    public class ClassMappers : Profile
    {
        public ClassMappers()
        {
            CreateMap<DataSet, CredentialsDto>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Tables["Credentials"].Rows[0]["User_ID"].ToString()))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Tables["Credentials"].Rows[0]["Password"].ToString()))
            .ForMember(dest => dest.ClientType, opt => opt.MapFrom(src => src.Tables["Credentials"].Rows[0]["ClientType"].ToString()))
            .ForMember(dest => dest.IsAuthenticated, opt => opt.MapFrom(src => src.Tables["Credentials"].Rows[0]["IsAuthenticated"].ToString()))
            .ForMember(dest => dest.IsFirstLogin, opt => opt.MapFrom(src => src.Tables["Credentials"].Rows[0]["IsFirstLogin"].ToString()))
            .ForMember(dest => dest.SessionID, opt => opt.MapFrom(src => src.Tables["Credentials"].Rows[0]["SessionID"].ToString()));

            CreateMap<DataSet, cUserIdent>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Tables["UserIdent"].Rows[0]["FullName"].ToString()))
            .ForMember(dest => dest.Pin, opt => opt.MapFrom(src => src.Tables["UserIdent"].Rows[0]["Pin"].ToString()))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Tables["UserIdent"].Rows[0]["Role"].ToString()))
            .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Tables["UserIdent"].Rows[0]["Language"].ToString()))
            .ForMember(dest => dest.RoleID, opt => opt.MapFrom(src => GetRoleID(src.Tables["Codes"].Rows[0]["Code"].ToString())))
            .ForMember(dest => dest.LoggedDate, opt => opt.MapFrom(src => DateTime.Now.ToShortDateString()));

        }
        private string GetRoleID(string code)
        {
            return code.Split("-")[0];
        }
    }
}
