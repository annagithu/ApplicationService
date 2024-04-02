using ApplicationService.InternalContracts.Application.Models;

namespace ApplicationService.App.InternalContracts.Application
{
    public class ErrorModels
    {
       public static ApplicationModel DoesntExist = new ApplicationModel() { 
           Name = "Invalid Value. Please, try again",
           Description = "Application with this ID  doesn't exist"
       };

        public static ApplicationModel AlreadyHaveaDraft = new ApplicationModel()
        {
            Name = "Invalid Value. Please, try again",
            Description = "You already have a draft"
        };

        public static ApplicationModel WrongActivity(ApplicationModel source) 
        {
            return new ApplicationModel()
            {
                Name = "Invalid Value. Please, try again",
                Description = $"You cannot choose {source.Activity} type of activity. Available types: {string.Join(" ", ActivityNames.AvailableNames)}"
            };
        }

        public static ApplicationModel AlreadySubmitted = new ApplicationModel()
        {
            Name = "Invalid Value. Please, try again",
            Description = "This application is submit already.You cannot delete/edit it."
        };


    }
}
