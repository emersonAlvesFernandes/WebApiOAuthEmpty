using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace WebApiOAuthEmpty
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var user = context.UserName;
            var password = context.Password;

            try
            {
                if (user != "R2088" || password != "xpto")
                {
                    context.SetError("invalid_grant", "Usuário ou senha inválidos");
                    return;
                }

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                identity.AddClaim(new Claim(ClaimTypes.Name, user));


                //Retornar do banco de dados
                var roles = new List<string>();
                roles.Add("Admin");

                foreach (var role in roles)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                }

                var principal = new GenericPrincipal(identity, roles.ToArray());

                //Se não executar a linha abaixo, não é possível obter os dados no controller
                Thread.CurrentPrincipal = principal;

                context.Validated(identity);
            }
            catch (Exception ex)
            {
                context.SetError("invalid_grant", "Falha ao Autenticar");
            }
        }
    }
}