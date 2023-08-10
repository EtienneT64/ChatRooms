using ChatRooms.Data;
using ChatRooms.Interfaces;
using ChatRooms.Models;
using ChatRooms.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatRooms.Controllers
{
    public class ChatroomsController : Controller
    {
        private readonly ChatroomContext _context;
        private readonly IMessageRepository _messageRepository;

        public ChatroomsController(ChatroomContext context, IMessageRepository messageRepository)
        {
            _context = context;
            _messageRepository = messageRepository;
        }

        // GET: Chatrooms
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return _context.Chatrooms != null ?
                        View(await _context.Chatrooms.ToListAsync()) :
                        Problem("Entity set 'ChatroomContext.Chatrooms'  is null.");
        }

        // GET: Chatrooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Chatrooms == null)
            {
                return NotFound();
            }

            var chatroom = await _context.Chatrooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chatroom == null)
            {
                return NotFound();
            }

            return View(chatroom);
        }

        // GET: Chatrooms/Chat/1
        public async Task<IActionResult> Chat(int id)
        {
            var messages = await _messageRepository.GetMessagesByChatroomId(id);
            var currUserId = HttpContext.User.GetUserId();
            var sendDate = DateTime.Now;

            var chatViewModel = new ChatViewModel
            {
                Messages = messages,
                CreateMessage = new CreateMessageViewModel
                {
                    ChatroomId = id,
                    UserId = currUserId,
                    SendDate = sendDate,

                }
            };

            return View(chatViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Chat(CreateMessageViewModel messageViewModel)
        {
            if (ModelState.IsValid)
            {
                var message = new Message
                {
                    Content = messageViewModel.Content,
                    MsgLength = messageViewModel.Content.Length,
                    SendDate = messageViewModel.SendDate,
                    UserId = messageViewModel.UserId,
                    ChatroomId = messageViewModel.ChatroomId,
                };

                _messageRepository.Add(message);
                return RedirectToAction("Chat");
            }
            else
            {
                ModelState.AddModelError("", "Message is invalid");
                var chatViewModel = new ChatViewModel
                {
                    Messages = await _messageRepository.GetMessagesByChatroomId(messageViewModel.ChatroomId),
                    CreateMessage = messageViewModel
                };

                return View(messageViewModel);
            }
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> SendMessage()
        //{

        //}

        // GET: Chatrooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Chatrooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,UserLimit,MsgLengthLimit")] Chatroom chatroom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chatroom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chatroom);
        }

        // GET: Chatrooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Chatrooms == null)
            {
                return NotFound();
            }

            var chatroom = await _context.Chatrooms.FindAsync(id);
            if (chatroom == null)
            {
                return NotFound();
            }
            return View(chatroom);
        }

        // POST: Chatrooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,UserLimit,MsgLengthLimit")] Chatroom chatroom)
        {
            if (id != chatroom.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chatroom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChatroomExists(chatroom.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(chatroom);
        }

        // GET: Chatrooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Chatrooms == null)
            {
                return NotFound();
            }

            var chatroom = await _context.Chatrooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chatroom == null)
            {
                return NotFound();
            }

            return View(chatroom);
        }

        // POST: Chatrooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Chatrooms == null)
            {
                return Problem("Entity set 'ChatroomContext.Chatrooms'  is null.");
            }
            var chatroom = await _context.Chatrooms.FindAsync(id);
            if (chatroom != null)
            {
                _context.Chatrooms.Remove(chatroom);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChatroomExists(int id)
        {
            return (_context.Chatrooms?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
