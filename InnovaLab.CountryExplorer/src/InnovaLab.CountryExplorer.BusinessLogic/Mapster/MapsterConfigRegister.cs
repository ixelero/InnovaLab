using Mapster;

namespace InnovaLab.CountryExplorer.BusinessLogic.Mapster;

public class MapsterConfigRegister : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ConfigContryMapping();
        config.ConfigFlagMapsMapping();
    }
}
