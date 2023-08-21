using ChatRooms.Helpers;
using ChatRooms.Hubs;
using ChatRooms.Interfaces;
using ChatRooms.Models;
using ChatRooms.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace ChatRooms.Controllers
{
    public class ChatroomsController : Controller
    {
        private readonly IChatroomRepository _chatroomRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHubContext<ChatHub> _chatHubContext;
        private readonly IPhotoService _photoService;

        public ChatroomsController(IChatroomRepository chatroomRepository, IMessageRepository messageRepository, IUserRepository userRepository, IHubContext<ChatHub> chatHubContext, IPhotoService photoService)
        {
            _chatroomRepository = chatroomRepository;
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _chatHubContext = chatHubContext;
            _photoService = photoService;
        }

        // GET: Chatrooms
        [Authorize]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var chatrooms = _chatroomRepository.GetAllQuery();

            if (!String.IsNullOrEmpty(searchString))
            {
                chatrooms = chatrooms.Where(c => c.Name.ToUpper().Contains(searchString.ToUpper()) || c.Description.ToUpper().Contains(searchString.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    chatrooms = chatrooms.OrderByDescending(c => c.Name);
                    break;
                default:
                    chatrooms = chatrooms.OrderBy(c => c.Name);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<Chatroom>.CreateAsync(chatrooms.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Chatrooms/Details/1
        [Authorize]
        public async Task<IActionResult> Details(int id)
        {

            Chatroom chatroom = await _chatroomRepository.GetByIdAsync(id);
            var ownerId = chatroom.OwnerId;
            var user = await _userRepository.GetUserByIdAsync(ownerId);
            var chatroomDetailsVM = new ChatroomDetailsViewModel
            {
                Id = id,
                Name = chatroom.Name,
                Description = chatroom.Description,
                MsgLengthLimit = chatroom.MsgLengthLimit,
                ChatroomImageUrl = chatroom.ChatroomImageUrl,
                OwnerUserName = user.UserName,
                OwnerId = ownerId,
            };
            return View(chatroomDetailsVM);
        }

        // GET: Chatrooms/Chat/1
        [Authorize]
        public async Task<IActionResult> Chat(int id)
        {
            var messages = await _messageRepository.GetMessagesByChatroomIdTake(id, 25);
            var chatroom = await _chatroomRepository.GetByIdAsync(id);
            var userId = HttpContext.User.GetUserId();
            var user = await _userRepository.GetUserByIdAsync(userId);
            var chatViewModel = new ChatViewModel
            {
                UserId = userId,
                UserName = user.UserName,
                ChatroomId = id,
                ChatroomName = chatroom.Name,
                Messages = messages,
            };

            return View(chatViewModel);
        }

        // GET: Chatrooms/Edit/1
        public async Task<IActionResult> Edit(int? id)
        {
            var chatroom = await _chatroomRepository.GetByIdAsync(id);
            if (chatroom == null) return View("Error");
            var ownerId = chatroom.OwnerId;

            if (HttpContext.User.GetUserId() != ownerId)
            {
                return RedirectToAction("Index");
            }

            var user = await _userRepository.GetUserByIdAsync(ownerId);
            var chatroomDetailsVM = new ChatroomDetailsViewModel
            {
                Name = chatroom.Name,
                Description = chatroom.Description,
                MsgLengthLimit = chatroom.MsgLengthLimit,
                ChatroomImageUrl = chatroom.ChatroomImageUrl,
                OwnerUserName = user.UserName,
                OwnerId = ownerId,
            };
            return View(chatroomDetailsVM);
        }

        // POST: Chatrooms/Edit/1
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ChatroomDetailsViewModel chatRoomDetailsVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit chatroom");
                return View("Edit", chatRoomDetailsVM);
            }

            var userChatroom = await _chatroomRepository.GetByIdAsyncNoTracking(id);

            if (userChatroom == null)
            {
                return View("Error");
            }

            var photoResult = await _photoService.AddPhotoAsync(chatRoomDetailsVM.Image);

            if (photoResult.Error != null)
            {
                ModelState.AddModelError("Image", "Photo upload failed");
                return View(chatRoomDetailsVM);
            }

            if (!string.IsNullOrEmpty(userChatroom.ChatroomImageUrl))
            {
                _ = _photoService.DeletePhotoAsync(userChatroom.ChatroomImageUrl);
            }

            var chatroom = new Chatroom
            {
                Id = id,
                Name = chatRoomDetailsVM.Name,
                Description = chatRoomDetailsVM.Description,
                MsgLengthLimit = chatRoomDetailsVM.MsgLengthLimit,
                ChatroomImageUrl = photoResult.Url.ToString(),
                OwnerId = chatRoomDetailsVM.OwnerId,
            };

            _chatroomRepository.Update(chatroom);

            return RedirectToAction("Index");
        }

        // GET: Chatrooms/Delete/1
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            var chatroom = await _chatroomRepository.GetByIdAsync(id);
            if (chatroom == null) return View("Error");
            var ownerId = chatroom.OwnerId;

            if (HttpContext.User.GetUserId() != ownerId)
            {
                return RedirectToAction("Index");
            }

            var user = await _userRepository.GetUserByIdAsync(ownerId);
            var chatroomDetailsVM = new ChatroomDetailsViewModel
            {
                Name = chatroom.Name,
                Description = chatroom.Description,
                MsgLengthLimit = chatroom.MsgLengthLimit,
                ChatroomImageUrl = chatroom.ChatroomImageUrl,
                OwnerUserName = user.UserName,
                OwnerId = ownerId,
            };
            return View(chatroomDetailsVM);
        }

        // POST: Chatrooms/Delete/1
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chatRoomDetails = await _chatroomRepository.GetByIdAsync(id);
            if (chatRoomDetails == null) return View("Error");

            _chatroomRepository.Delete(chatRoomDetails);
            return RedirectToAction("Index");
        }

    }
}
