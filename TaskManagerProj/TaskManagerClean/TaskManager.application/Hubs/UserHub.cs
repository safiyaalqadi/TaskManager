using Microsoft.AspNetCore.SignalR;

namespace TaskManagerClean.API.Hubs
{
    public class UserHub : Hub
{
   
      /* public async Task SendUserUpdate(string message)
       {
         await Clients.All.SendAsync("ReceiveUserUpdate", message);
       }
      */
        public async Task SendNewTask(string userName,string message)
        {
            await Clients.All.SendAsync("ReceiveNewTask",userName, message);
        }

    }

}
