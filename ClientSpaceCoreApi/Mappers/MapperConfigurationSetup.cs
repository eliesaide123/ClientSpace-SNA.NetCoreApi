using AutoMapper;
using ClientSpaceCoreApi.Mappers;

public class MapperConfigurationSetup
{
    public static MapperConfiguration Configure()
    {
        return new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ClassMappers>();
        });
    }
}
