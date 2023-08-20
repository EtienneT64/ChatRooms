using ChatRooms.Hubs;
using ChatRooms.Interfaces;
using ChatRooms.Models;
using ChatRooms.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatRooms.Controllers
{
    public class ChatroomsController : Controller
    {
        private readonly IChatroomRepository _chatroomRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHubContext<ChatHub> _chatHubContext;

        public ChatroomsController(IChatroomRepository chatroomRepository, IMessageRepository messageRepository, IUserRepository userRepository, IHubContext<ChatHub> chatHubContext)
        {
            _chatroomRepository = chatroomRepository;
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _chatHubContext = chatHubContext;
        }

        // GET: Chatrooms
        [Authorize]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Chatroom> chatrooms = await _chatroomRepository.GetAll();
            return View(chatrooms);
        }

        // GET: Chatrooms/Details/1
        public async Task<IActionResult> Details(int? id)
        {

            Chatroom chatroom = await _chatroomRepository.GetByIdAsync(id);
            var user = await _userRepository.GetUserByIdAsync(chatroom.OwnerId);
            var chatroomDetailsVM = new ChatroomDetailsViewModel
            {
                Name = chatroom.Name,
                Description = chatroom.Description,
                MsgLengthLimit = chatroom.MsgLengthLimit,
                ChatroomImageUrl = chatroom.ChatroomImageUrl,
                OwnerUserName = user.UserName,
            };
            return View(chatroomDetailsVM);
        }

        // GET: Chatrooms/Chat/1
        [Authorize]
        public async Task<IActionResult> Chat(int id)
        {
            var messages = await _messageRepository.GetMessagesByChatroomId(id);
            var chatroom = await _chatroomRepository.GetByIdAsync(id);

            var chatViewModel = new ChatViewModel
            {
                UserId = HttpContext.User.GetUserId(),
                ChatroomId = id,
                ChatroomName = chatroom.Name,
                Messages = messages,
            };

            return View(chatViewModel);
        }

        //public async Task<IActionResult> Delete(int? id)
        //{
        //    var chatroomDetails = await _chatroomRepository.GetByIdAsync(id);
        //    if (chatroomDetails == null) return View("Error");
        //    return View(chatroomDetails);
        //}

        //// POST: Chatrooms/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var chatRoomDetails = await _chatroomRepository.GetByIdAsync(id);
        //    if (chatRoomDetails == null) return View("Error");

        //    _chatroomRepository.Delete(chatRoomDetails);
        //    return RedirectToAction("Index");
        //}

    }
}
