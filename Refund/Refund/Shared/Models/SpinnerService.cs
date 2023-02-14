using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KNUST_Medical_Refund.Shared.Models
{
	public class SpinnerService
	{
		public event Action OnShow;
		public event Action OnHide;

		public void Show()
		{
			OnShow?.Invoke();
		}

		public void Hide()
		{
			OnHide?.Invoke();
		}
	}
}
