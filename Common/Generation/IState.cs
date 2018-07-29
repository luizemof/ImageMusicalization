using Common.Extraction;
using Common.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Generation
{
	public interface IState
	{
		List<LinkedState> LinkedStates { get; set; }
		Guid Guid { get; }
		EModelType ModelType { get; }
		bool IsDeadState { get; }

		void CalculateLinkedStatesProbability();
	}
}
