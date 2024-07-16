namespace Bengbenz.Embassy.eServices.Infrastructure.Data.Config;

public class EmbassyDbDesignTimeFactory : BaseDesignTimeFactory<EmbassyDbContext>
{
    protected override string ConnectionStringKey => "DefaultConnection";
}