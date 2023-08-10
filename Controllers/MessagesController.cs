using ChatRooms.Interfaces;
using ChatRooms.Models;
using ChatRooms.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChatRooms.Controllers
{
    public class MessagesController : Controller
    {
        //private readonly ChatroomContext _context;
        private readonly UserManager<User> _userManager;

        private readonly IMessageRepository _messageRepository;

        public MessagesController(IMessageRepository messageRepository, UserManager<User> userManager)
        {
            //_context = context;
            _messageRepository = messageRepository;
            _userManager = userManager;
        }

        // GET: Messages
        public async Task<IActionResult> Index()
        {
            IEnumerable<Message> messages = await _messageRepository.GetAll();
            return View(messages);
        }

        // GET: Messages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Message message = await _messageRepository.GetByIdAsync(id);
            return View(message);
        }


        // GET: Chatrooms/Chat/1
        // GET: Messages/Create
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null || _messageRepository.GetMessagesByChatroomId == null)
            {
                return NotFound();
            }

            // Use LINQ to select messages with ChatroomId = 1
            var messagesInChatroom = _messageRepository.GetMessagesByChatroomId(id);
            return View(messagesInChatroom);
        }

        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMessage(CreateMessageViewModel messageViewModel)
        {
            if (ModelState.IsValid)
            {
                var message = new Message
                {
                    Content = messageViewModel.Content,
                    MsgLength = messageViewModel.Content.Length,
                    SendDate = messageViewModel.SendDate,
                    UserId = _userManager.GetUserId(User), // Get the UserId from UserManager
                    ChatroomId = messageViewModel.ChatroomId,
                };

                _messageRepository.Add(message);
                return RedirectToAction("Chat", new { id = messageViewModel.ChatroomId }); // Redirect to Chat action
            }
            var chatViewModel = new ChatViewModel
            {
                Messages = await _messageRepository.GetMessagesByChatroomId(messageViewModel.ChatroomId),
                CreateMessage = messageViewModel
            };
            return View("Chat", chatViewModel);
        }


        //// GET: Messages/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.Messages == null)
        //    {
        //        return NotFound();
        //    }

        //    var message = await _context.Messages.FindAsync(id);
        //    if (message == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["ChatroomId"] = new SelectList(_context.Chatrooms, "Id", "Description", message.ChatroomId);

        //    return View(message);
        //}

        //// POST: Messages/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,ChatroomId,Content,MsgLength,SendDate")] Message message)
        //{
        //    if (id != message.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(message);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!MessageExists(message.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ChatroomId"] = new SelectList(_context.Chatrooms, "Id", "Description", message.ChatroomId);
        //    return View(message);
        //}

        //// GET: Messages/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Messages == null)
        //    {
        //        return NotFound();
        //    }

        //    var message = await _context.Messages
        //        .Include(m => m.Chatroom)
        //        .Include(m => m.User)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (message == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(message);
        //}

        //// POST: Messages/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Messages == null)
        //    {
        //        return Problem("Entity set 'ChatroomContext.Messages'  is null.");
        //    }
        //    var message = await _context.Messages.FindAsync(id);
        //    if (message != null)
        //    {
        //        _context.Messages.Remove(message);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool MessageExists(int id)
        //{
        //    return (_context.Messages?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
    }
}
