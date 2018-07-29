using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
	/// <summary>
	/// Enumerador para identificar o modelo de execução para a extração e geração da música.
	/// </summary>
	public enum EModelType
	{
		Unknow = 0,

		[Description("KMeans")]
		KMeans = 1,
		[Description("SURF")]
		SURF = 2
	}
}
