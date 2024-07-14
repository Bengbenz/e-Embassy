namespace Bengbenz.Embassy.eServices.Infrastructure.Identity.Config;

public class AppIdentityDbDesignTimeFactory : BaseDesignTimeFactory<AppIdentityDbContext>
{
    protected override string ConnectionStringKey => "IdentityConnection";
}