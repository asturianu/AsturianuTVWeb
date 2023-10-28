using AsturianuTV.Infrastructure.Data.Models.Subscriptions;
using AsturianuTV.ViewModels.System.SubscriptionViewModels;
using AutoMapper;

namespace AsturianuTV.MapperProfile
{
    public class SubscriptionProfile : Profile
    {
        public SubscriptionProfile()
        {
            CreateMap<CreateSubscriptionViewModel, Subscription>();
            CreateMap<UpdateSubscriptionViewModel, Subscription>();
        }
    }
}
