using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SecureNotes.Data;
using SecureNotes.Models;
using System.Linq;
using System.Security.Claims;

namespace SecureNotes.Controllers
{
    [Authorize]
    public class NotesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly EncryptionService _encryption;

        public NotesController(AppDbContext context, EncryptionService encryption)
        {
            _context = context;
            _encryption = encryption;
        }

        // =========================
        // INDEX
        // =========================
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var notes = _context.Notes
                .Where(n => n.UserId == userId)
                .ToList();

            foreach (var note in notes)
            {
                note.Title = SafeDecrypt(note.Title) ?? "[Sem título]";
                note.Content = SafeDecrypt(note.Content) ?? "";
            }

            return View(notes);
        }

        // =========================
        // CREATE (GET)
        // =========================
        public IActionResult Create()
        {
            return View();
        }

        // =========================
        // CREATE (POST)
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Note note)
        {
            if (!ModelState.IsValid)
                return View(note);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            note.UserId = userId;
            note.Title = _encryption.Encrypt(note.Title.Trim());
            note.Content = _encryption.Encrypt(note.Content.Trim());

            _context.Notes.Add(note);
            _context.SaveChanges();

            LogAction("CREATE", "Note", "Nota criada");

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // DETAILS
        // =========================
        public IActionResult Details(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var note = _context.Notes
                .FirstOrDefault(n => n.Id == id && n.UserId == userId);

            if (note == null)
                return NotFound();


            note.Title = SafeDecrypt(note.Title);
            note.Content = SafeDecrypt(note.Content);

            return View(note);
        }

        // =========================
        // EDIT (GET)
        // =========================
        public IActionResult Edit(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var note = _context.Notes
                .FirstOrDefault(n => n.Id == id && n.UserId == userId);

            if (note == null)
                return NotFound();

            note.Title = SafeDecrypt(note.Title);
            note.Content = SafeDecrypt(note.Content);

            return View(note);
        }

        // =========================
        // EDIT (POST)
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Note note)
        {
            if (!ModelState.IsValid)
                return View(note);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var existingNote = _context.Notes
                .FirstOrDefault(n => n.Id == id && n.UserId == userId);

            if (existingNote == null)
                return NotFound();

            existingNote.Title = _encryption.Encrypt(note.Title.Trim());
            existingNote.Content = _encryption.Encrypt(note.Content.Trim());

            _context.SaveChanges();

            LogAction("EDIT", "Note", "Nota editada");

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // DELETE (GET)
        // =========================
        public IActionResult Delete(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var note = _context.Notes
                .FirstOrDefault(n => n.Id == id && n.UserId == userId);

            if (note == null)
                return NotFound();

            note.Title = SafeDecrypt(note.Title);
            note.Content = SafeDecrypt(note.Content);

            return View(note);
        }

        // =========================
        // DELETE (POST)
        // =========================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var note = _context.Notes
                .FirstOrDefault(n => n.Id == id && n.UserId == userId);

            if (note == null)
                return NotFound();

            _context.Notes.Remove(note);
            _context.SaveChanges();

            LogAction("DELETE", "Note", "Nota apagada");

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // SAFE DECRYPT
        // =========================
        private string SafeDecrypt(string value)
        {
            try
            {
                return _encryption.Decrypt(value);
            }
            catch
            {
                return "[Erro ao ler nota]";
            }
        }

        // =========================
        // LOG DE AÇÕES
        // =========================
        private void LogAction(string action, string entity, string details)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var log = new AuditLog
            {
                UserId = userId,
                Action = action,
                Entity = entity,
                Details = details
            };

            _context.AuditLogs.Add(log);
            _context.SaveChanges();
        }
    }
}