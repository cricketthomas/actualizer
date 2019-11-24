using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using actualizer.Models;
using actualizer.Services;
namespace actualizer.Services {
    public interface IOktaService {

        Task<IEnumerable<OktaModel>> GetItems(string userId);

        //Task AddItem(string userId, string text);

        //Task UpdateItem(string userId, Guid id, TodoItemModel updatedData);

        //Task DeleteItem(string userId, Guid id);
    }
}