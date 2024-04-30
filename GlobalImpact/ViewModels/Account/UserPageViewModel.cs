using GlobalImpact.Models;
using System.ComponentModel.DataAnnotations;

namespace GlobalImpact.ViewModels.Account
{
    public class UserPageViewModel
    {
        public AppUser user;

        public ProductTransactions productTransactions;
        public bool confirmProductTransactions;


		public RecyclingTransaction recyclingTransaction;
		public bool confirmRecyclingTransaction;

		public List<string> produtos;

		public List<int> quantidades;

		public List<int> precoProduto;
	}
}
