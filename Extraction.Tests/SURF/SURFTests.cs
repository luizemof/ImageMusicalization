using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Common.Extraction;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Extraction.Tests.SURF
{
	[TestClass]
	public class SURFTests
	{
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void SURF_Execute_Empty_Image()
		{
			Extraction.SURF.SURF surf = new Extraction.SURF.SURF("");
		}

		[TestMethod]
		public void SURF_Execute_Image()
		{
			// Arrange
			List<IResult> expectedResults = new List<IResult>()
			{
				new Extraction.SURF.SURFResult()
				{
					Coordinator = new Point(28, 11),
					Note = ENote.C,
					NumberOfElements = 3
				},
				new Extraction.SURF.SURFResult()
				{
					Coordinator = new Point(31, 32),
					Note = ENote.Dm,
					NumberOfElements = 5
				},
				new Extraction.SURF.SURFResult()
				{
					Coordinator = new Point(81, 31),
					Note = ENote.F,
					NumberOfElements = 9
				}
			};
			Extraction.SURF.SURF surf = new Extraction.SURF.SURF(Constants.ImageFile);

			// Act
			surf.Execute();

			// Assert
			Assert.IsFalse(surf.Result.IsNullOrEmpty());
			Assert.AreEqual(expectedResults.Count, surf.Result.Count);

			List<IResult> orderedList = surf.Result.OrderBy(r => r.NumberOfElements).ToList();
			for (int i = 0; i < surf.Result.Count; i++)
			{
				Assert.AreEqual(expectedResults[i].Coordinator.X, orderedList[i].Coordinator.X);
				Assert.AreEqual(expectedResults[i].Coordinator.Y, orderedList[i].Coordinator.Y);
				Assert.AreEqual(expectedResults[i].Note, orderedList[i].Note);
				Assert.AreEqual(expectedResults[i].NumberOfElements, orderedList[i].NumberOfElements);
			}
		}
	}
}
