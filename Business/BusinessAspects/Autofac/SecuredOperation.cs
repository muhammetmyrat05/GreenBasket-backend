using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Core.Extensions;
using Business.Constants;

namespace Business.BusinessAspects.Autofac
{
    // 🔐 Role-based security interceptor (JWT)
    public class SecuredOperation : MethodInterception
    {
        private readonly string[] _roles;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(','); // örn: "Admin,User"
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
        }

        protected override void OnBefore(IInvocation invocation)
        {
            var user = _httpContextAccessor.HttpContext.User;

            if (user?.Identity == null || !user.Identity.IsAuthenticated)
                throw new Exception(Messages.UserNotAuthenticated);

            var roleClaims = user.ClaimRoles(); // JWT içindeki roller
            if (roleClaims == null || !roleClaims.Any())
                throw new Exception(Messages.AuthorizationDenied);

            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                    return;
            }

            // ❌ Eğer kullanıcı gerekli role sahip değilse
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
