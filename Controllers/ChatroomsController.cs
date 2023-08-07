using ChatRooms.Data;
using ChatRooms.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatRooms.Controllers
{
    public class ChatroomsController : Controller
    {
        private readonly ChatroomContext _context;

        public ChatroomsController(ChatroomContext context)
        {
            _context = context;
        }

        // GET: Chatrooms
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
        public async Task<IActionResult> Chat(int? id)
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


            // Use LINQ to select messages with ChatroomId = 1
            var messagesInChatroom = await _context.Messages
                .Where(message => message.ChatroomId == id) // Filter messages by ChatroomId
                .ToListAsync();

            return View(messagesInChatroom);
        }

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
