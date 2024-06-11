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
            .ForMember(dest => dest.IsAuthenticated, opt => opt.MapFrom(src => ConvertToBoolean(src.Tables["Credentials"].Rows[0]["IsAuthenticated"].ToString())))
            .ForMember(dest => dest.IsFirstLogin, opt => opt.MapFrom(src => ConvertToBoolean(src.Tables["Credentials"].Rows[0]["IsFirstLogin"].ToString())))
            .ForMember(dest => dest.SessionID, opt => opt.MapFrom(src => src.Tables["Credentials"].Rows[0]["SessionID"].ToString()));

            CreateMap<DataSet, cUserIdent>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Tables["UserIdent"].Rows[0]["FullName"].ToString()))
            .ForMember(dest => dest.Pin, opt => opt.MapFrom(src => src.Tables["UserIdent"].Rows[0]["Pin"].ToString()))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Tables["UserIdent"].Rows[0]["Role"].ToString()))
            .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.Tables["UserIdent"].Rows[0]["Language"].ToString()))
            .ForMember(dest => dest.RoleID, opt => opt.MapFrom(src => GetRoleID(src.Tables["Codes"].Rows[0]["Code"].ToString())))
            .ForMember(dest => dest.LoggedDate, opt => opt.MapFrom(src => DateTime.Now.ToShortDateString()));

            CreateMap<DataSet, UserAccount>()
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Tables["TPIDENT"].Rows[0]["TP-UserId"].ToString()))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Tables["TPIDENT"].Rows[0]["TP-Pwd"].ToString()))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Tables["TPIDENT"].Rows[0]["TP-Email"].ToString()))
            .ForMember(dest => dest.Mobile, opt => opt.MapFrom(src => src.Tables["TPIDENT"].Rows[0]["TP-Mobile"].ToString()))
            .ForMember(dest => dest.UserLang, opt => opt.MapFrom(src => ConvertToInt(src.Tables["TPIDENT"].Rows[0]["TP-UserLang"].ToString())))
            .ForMember(dest => dest.ContactScenario, opt => opt.MapFrom(src => src.Tables["TPIDENT"].Rows[0]["TP-ContactScenario"].ToString()))
            .ForMember(dest => dest.RegType, opt => opt.MapFrom(src => src.Tables["TPIDENT"].Rows[0]["TP-RegType"].ToString()))
            .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.Tables["TPVALIDSET"].Rows[0]["TP-Question"].ToString()))
            .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src.Tables["TPVALIDSET"].Rows[0]["TP-Answer"].ToString()));


            CreateMap<DataTable, string[]>()
            .ConvertUsing(src =>
                src.AsEnumerable()
                   .Where(row => row.Field<string>("Tbl_Name") == "_CSQuestions")
                   .Select(row => row.Field<string>("Eng_Full"))
                   .ToArray()
            );

            CreateMap<DataSet, Person>()
            .ForMember(dest => dest.PIN, opt => opt.MapFrom(src => ConvertToInt(src.Tables["Persons"].Rows[0]["PIN"].ToString())))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Tables["Persons"].Rows[0]["Age"].ToString()))
            .ForMember(dest => dest.Marital, opt => opt.MapFrom(src => src.Tables["Persons"].Rows[0]["Marital"].ToString()))
            .ForMember(dest => dest.Per_Title, opt => opt.MapFrom(src => src.Tables["Persons"].Rows[0]["Per_Title"].ToString()))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Tables["Persons"].Rows[0]["FirstName"].ToString()))
            .ForMember(dest => dest.Father, opt => opt.MapFrom(src => src.Tables["Persons"].Rows[0]["Father"].ToString()))
            .ForMember(dest => dest.Family, opt => opt.MapFrom(src => src.Tables["Persons"].Rows[0]["Family"].ToString()))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Tables["Persons"].Rows[0]["FullName"].ToString()))
            .ForMember(dest => dest.Profession, opt => opt.MapFrom(src => src.Tables["Persons"].Rows[0]["Profession"].ToString()))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Tables["Persons"].Rows[0]["Address"].ToString()))
            .ForMember(dest => dest.EntityType, opt => opt.MapFrom(src => src.Tables["Persons"].Rows[0]["EntityType"].ToString()))
            .ForMember(dest => dest.DOB_Day, opt => opt.MapFrom(src => ConvertToInt(src.Tables["Persons"].Rows[0]["DOB_Day"].ToString())))
            .ForMember(dest => dest.DOB_Month, opt => opt.MapFrom(src => ConvertToInt(src.Tables["Persons"].Rows[0]["DOB_Month"].ToString())))
            .ForMember(dest => dest.DOB_Year, opt => opt.MapFrom(src => ConvertToInt(src.Tables["Persons"].Rows[0]["DOB_Year"].ToString())))
            .ForMember(dest => dest.HasRequest, opt => opt.MapFrom(src => ConvertToBoolean(src.Tables["Persons"].Rows[0]["HasRequest"].ToString())))
            .ForMember(dest => dest.HasUnpaid, opt => opt.MapFrom(src => ConvertToBoolean(src.Tables["Persons"].Rows[0]["HasUnpaid"].ToString())))
            .ForMember(dest => dest.HasClaims, opt => opt.MapFrom(src => ConvertToBoolean(src.Tables["Persons"].Rows[0]["HasClaims"].ToString())))
            .ForMember(dest => dest.HasRenewal, opt => opt.MapFrom(src => ConvertToBoolean(src.Tables["Persons"].Rows[0]["HasRenewal"].ToString())))
            .ForMember(dest => dest.HasFresh, opt => opt.MapFrom(src => ConvertToBoolean(src.Tables["Persons"].Rows[0]["HasFresh"].ToString())))
            .ForMember(dest => dest.KYC, opt => opt.MapFrom(src => ConvertToBoolean(src.Tables["Persons"].Rows[0]["KYC"].ToString())))
            .ForMember(dest => dest.ShowProfile, opt => opt.MapFrom(src => ConvertToBoolean(src.Tables["Persons"].Rows[0]["ShowProfile"].ToString())))
            .ForMember(dest => dest.ShowMissing, opt => opt.MapFrom(src => ConvertToBoolean(src.Tables["Persons"].Rows[0]["ShowMissing"].ToString())))
            .ForMember(dest => dest.AgentSOA, opt => opt.MapFrom(src => ConvertToBoolean(src.Tables["Persons"].Rows[0]["AgentSOA"].ToString())))
            .ForMember(dest => dest.RPSEnabled, opt => opt.MapFrom(src => ConvertToBoolean(src.Tables["Persons"].Rows[0]["RPSEnabled"].ToString())))
            .ForMember(dest => dest.YearMonth, opt => opt.MapFrom(src => src.Tables["Persons"].Rows[0]["YearMonth"].ToString()))
            .ForMember(dest => dest.KYCMSG, opt => opt.MapFrom(src => src.Tables["Persons"].Rows[0]["KYCMSG"].ToString()))
            .ForMember(dest => dest.CONVERT_DATA, opt => opt.MapFrom(src => src.Tables["Persons"].Rows[0]["CONVERT_DATA"].ToString()));

        }
        private string GetRoleID(string code)
        {
            return code.Split("-")[0];
        }

        private int? ConvertToInt(string str)
        {
            if (str == string.Empty) return null;
            return Convert.ToInt32(str);
        }
        private decimal ConvertToDecimal(string str)
        {
            return Convert.ToDecimal(str);
        }
        private double ConvertToDouble(string str)
        {
            return Convert.ToDouble(str);
        }
        private float ConvertToFloat(string str)
        {
            return (float) Convert.ToDouble(str);
        }
        private bool ConvertToBoolean(string str)
        {
            if(str == string.Empty) return false;
            return Convert.ToBoolean(str);
        }
    }
}
