using Microsoft.AspNetCore.Authorization;

namespace IETTJWT.API
{
    public class ExchangeRequirement : IAuthorizationRequirement
    {
    }


    public class ExchangeRequirementHandler : AuthorizationHandler<ExchangeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ExchangeRequirement requirement)
        {

            if (context.User != null && context.User.Identity != null)
            {
                var claim = context.User.Claims.FirstOrDefault(x => x.Type == "birthDay");

                if (claim == null)
                {
                    context.Fail();
                }

                var firtDateTime = DateTime.Now;
                var claimDateTime = Convert.ToDateTime(claim.Value);

                var result = (firtDateTime - claimDateTime);
                if (result.TotalDays > 365 * 18)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }



            }
            else
            {
                context.Fail();
            }
            return Task.CompletedTask;


        }
    }



}
