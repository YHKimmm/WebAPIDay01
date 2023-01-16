using Microsoft.AspNetCore.Mvc;
using WebAPIDay01.Models;

namespace WebAPIDay01.Repository
{
    public class ToDoRepository
    {
        private readonly TodoContext _context;
        public ToDoRepository(TodoContext context)
        {
            _context = context;
        }

        public ICollection<ToDo> GetAllToDos()
        {
            return _context.ToDos.ToList();
        }

        public ToDo GetToDo(int id)
        {
            return _context.ToDos.Where(td => td.Id == id).FirstOrDefault();
        }

        public void AddToDo(ToDo toDo)
        {
           
           _context.Add(toDo);
           _context.SaveChanges();
        }

        public bool UpdateToDo(ToDo toDo)
        {

            var getToDo = GetToDo(toDo.Id);
           

            if(getToDo == null)
            {
                return false;
            }

            getToDo.Description = toDo.Description;
            getToDo.Priority = toDo.Priority;
            getToDo.IsComplete = toDo.IsComplete;
            _context.SaveChanges();

            return true;
        }

        public bool DeleteToDo(int id)
        {
            var toDoRecord = _context.ToDos.Where(td => td.Id == id).FirstOrDefault();

            try
            {
                _context.Remove(toDoRecord);
                _context.SaveChanges();
            }
            catch (Exception)
            {

                return false;
            }

            return true;
        }

    }
}
