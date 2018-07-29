using System;
using System.Collections.Generic;
using System.Drawing;
using Common;
using Common.Extraction;
using Common.Generation;
using Extraction.KMeans;
using Extraction.SURF;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Generation.Tests
{
	[TestClass]
	public class StateMachineExecutorTests
	{
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void StateMachineExecutor_Execute_ThrowArgumentException()
		{
			StateMachineExecutor executor = new StateMachineExecutor(null);
		}

		[TestMethod]
		public void StateMachineExecutor_SURF_Execute()
		{
			// Arrange
			StateMachineExecutor.Parameters parms = new StateMachineExecutor.Parameters()
			{
				Results = new List<IResult>()
				{
					new SURFResult()
					{
						Coordinator = new Point(1, 23),
						Note = ENote.C,
						NumberOfElements = 100,
						Pixel = Color.Red
					},
					new SURFResult()
					{
						Coordinator = new Point(1, 40),
						Note = ENote.Dm,
						NumberOfElements = 87,
						Pixel = Color.Yellow
					}
				},
				MaxNotes = 30,
				TargetType = EModelType.SURF
			};

			// Act
			List<IState> states = new StateMachineExecutor(parms).Execute();

			// Assert
			Assert.IsNotNull(states);
			Assert.AreEqual(30, states.Count);
			states.ForEach(s =>
			{
				Assert.IsFalse(s.LinkedStates.IsNullOrEmpty());
				Assert.AreEqual(1, s.LinkedStates.Count);
			});
		}

		[TestMethod]
		public void StateMachineExecutor_SURF_Five_Results_Execute()
		{
			// Arrange
			StateMachineExecutor.Parameters parms = new StateMachineExecutor.Parameters()
			{
				Results = new List<IResult>()
				{
					new SURFResult()
					{
						Coordinator = new Point(1, 23),
						Note = ENote.C,
						NumberOfElements = 100,
						Pixel = Color.Red
					},
					new SURFResult()
					{
						Coordinator = new Point(1, 40),
						Note = ENote.Dm,
						NumberOfElements = 87,
						Pixel = Color.Yellow
					},
					new SURFResult()
					{
						Coordinator = new Point(40, 23),
						Note = ENote.G,
						NumberOfElements = 87,
						Pixel = Color.Yellow
					},
					new SURFResult()
					{
						Coordinator = new Point(15, 44),
						Note = ENote.Dm,
						NumberOfElements = 87,
						Pixel = Color.Yellow
					},
					new SURFResult()
					{
						Coordinator = new Point(15, 44),
						Note = ENote.Dm,
						NumberOfElements = 87,
						Pixel = Color.Yellow
					}
				},
				MaxNotes = 30,
				TargetType = EModelType.SURF
			};

			// Act
			List<IState> states = new StateMachineExecutor(parms).Execute();

			// Assert
			Assert.IsNotNull(states);
			Assert.AreEqual(30, states.Count);
			states.ForEach(s =>
			{
				Assert.IsFalse(s.LinkedStates.IsNullOrEmpty());
				Assert.AreEqual(4, s.LinkedStates.Count);
			});
		}

		[TestMethod]
		public void StateMachineExecutor_KMeans_Five_Results_Execute()
		{
			// Arrange
			StateMachineExecutor.Parameters parms = new StateMachineExecutor.Parameters()
			{
				Results = new List<IResult>()
				{
					new KMeansResult()
					{
						Coordinator = new Point(1, 23),
						Note = ENote.C,
						NumberOfElements = 100,
						Pixel = Color.Red
					},
					new KMeansResult()
					{
						Coordinator = new Point(1, 40),
						Note = ENote.Dm,
						NumberOfElements = 87,
						Pixel = Color.Yellow
					},
					new KMeansResult()
					{
						Coordinator = new Point(40, 23),
						Note = ENote.G,
						NumberOfElements = 87,
						Pixel = Color.Yellow
					},
					new KMeansResult()
					{
						Coordinator = new Point(15, 44),
						Note = ENote.Dm,
						NumberOfElements = 87,
						Pixel = Color.Yellow
					},
					new KMeansResult()
					{
						Coordinator = new Point(15, 44),
						Note = ENote.Dm,
						NumberOfElements = 87,
						Pixel = Color.Yellow
					}
				},
				MaxNotes = 30,
				TargetType = EModelType.KMeans
			};

			// Act
			List<IState> states = new StateMachineExecutor(parms).Execute();

			// Assert
			Assert.IsNotNull(states);
			Assert.AreEqual(30, states.Count);
			states.ForEach(s =>
			{
				Assert.IsFalse(s.LinkedStates.IsNullOrEmpty());
				Assert.AreEqual(4, s.LinkedStates.Count);
			});
		}

		[TestMethod]
		public void StateMachineExecutor_KMeans_Execute()
		{
			// Arrange
			StateMachineExecutor.Parameters parms = new StateMachineExecutor.Parameters()
			{
				Results = new List<IResult>()
				{
					new KMeansResult()
					{
						Coordinator = new Point(1, 23),
						Note = ENote.C,
						NumberOfElements = 100,
						Pixel = Color.Red
					},
					new KMeansResult()
					{
						Coordinator = new Point(1, 40),
						Note = ENote.Dm,
						NumberOfElements = 87,
						Pixel = Color.Yellow
					}
				},
				MaxNotes = 30,
				TargetType = EModelType.KMeans
			};

			// Act
			List<IState> states = new StateMachineExecutor(parms).Execute();

			// Assert
			Assert.IsNotNull(states);
			Assert.AreEqual(30, states.Count);
			states.ForEach(s =>
			{
				Assert.IsFalse(s.LinkedStates.IsNullOrEmpty());
				Assert.AreEqual(1, s.LinkedStates.Count);
			});
		}

		[TestMethod]
		public void StateMachineExecutor_SURF_50_Max_Interaction_Execute()
		{
			// Arrange
			StateMachineExecutor.Parameters parms = new StateMachineExecutor.Parameters()
			{
				Results = new List<IResult>()
				{
					new SURFResult()
					{
						Coordinator = new Point(1, 23),
						Note = ENote.C,
						NumberOfElements = 100,
						Pixel = Color.Red
					},
					new SURFResult()
					{
						Coordinator = new Point(1, 40),
						Note = ENote.Dm,
						NumberOfElements = 87,
						Pixel = Color.Yellow
					},
					new SURFResult()
					{
						Coordinator = new Point(40, 23),
						Note = ENote.G,
						NumberOfElements = 87,
						Pixel = Color.Yellow
					},
					new SURFResult()
					{
						Coordinator = new Point(15, 44),
						Note = ENote.Dm,
						NumberOfElements = 87,
						Pixel = Color.Yellow
					},
					new SURFResult()
					{
						Coordinator = new Point(15, 44),
						Note = ENote.Dm,
						NumberOfElements = 87,
						Pixel = Color.Yellow
					}
				},
				MaxNotes = 50,
				TargetType = EModelType.SURF
			};

			// Act
			List<IState> states = new StateMachineExecutor(parms).Execute();

			// Assert
			Assert.IsNotNull(states);
			Assert.AreEqual(50, states.Count);
			states.ForEach(s =>
			{
				Assert.IsFalse(s.LinkedStates.IsNullOrEmpty());
				Assert.AreEqual(4, s.LinkedStates.Count);
			});
		}

		[TestMethod]
		public void StateMachineExecutor_KMeans_50_Max_Interaction_Execute()
		{
			// Arrange
			StateMachineExecutor.Parameters parms = new StateMachineExecutor.Parameters()
			{
				Results = new List<IResult>()
				{
					new KMeansResult()
					{
						Coordinator = new Point(1, 23),
						Note = ENote.C,
						NumberOfElements = 100,
						Pixel = Color.Red
					},
					new KMeansResult()
					{
						Coordinator = new Point(1, 40),
						Note = ENote.Dm,
						NumberOfElements = 87,
						Pixel = Color.Yellow
					},
					new KMeansResult()
					{
						Coordinator = new Point(40, 23),
						Note = ENote.G,
						NumberOfElements = 87,
						Pixel = Color.Yellow
					},
					new KMeansResult()
					{
						Coordinator = new Point(15, 44),
						Note = ENote.Dm,
						NumberOfElements = 87,
						Pixel = Color.Yellow
					},
					new KMeansResult()
					{
						Coordinator = new Point(15, 44),
						Note = ENote.Dm,
						NumberOfElements = 87,
						Pixel = Color.Yellow
					}
				},
				MaxNotes = 50,
				TargetType = EModelType.KMeans
			};

			// Act
			List<IState> states = new StateMachineExecutor(parms).Execute();

			// Assert
			Assert.IsNotNull(states);
			Assert.AreEqual(50, states.Count);
			states.ForEach(s =>
			{
				Assert.IsFalse(s.LinkedStates.IsNullOrEmpty());
				Assert.AreEqual(4, s.LinkedStates.Count);
			});
		}

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void StateMachineExecutor_KMeans_And_SURF_ThrowArgumentException()
		{
			// Arrange
			StateMachineExecutor.Parameters parms = new StateMachineExecutor.Parameters()
			{
				Results = new List<IResult>()
				{
					new KMeansResult()
					{
						Coordinator = new Point(1, 23),
						Note = ENote.C,
						NumberOfElements = 100,
						Pixel = Color.Red
					},
					new KMeansResult()
					{
						Coordinator = new Point(1, 40),
						Note = ENote.Dm,
						NumberOfElements = 87,
						Pixel = Color.Yellow
					},
					new KMeansResult()
					{
						Coordinator = new Point(40, 23),
						Note = ENote.G,
						NumberOfElements = 87,
						Pixel = Color.Yellow
					},
					new KMeansResult()
					{
						Coordinator = new Point(15, 44),
						Note = ENote.Dm,
						NumberOfElements = 87,
						Pixel = Color.Yellow
					},
					new SURFResult()
					{
						Coordinator = new Point(15, 44),
						Note = ENote.Dm,
						NumberOfElements = 87,
						Pixel = Color.Yellow
					}
				},
				MaxNotes = 50,
				TargetType = EModelType.KMeans
			};

			// Act
			List<IState> states = new StateMachineExecutor(parms).Execute();

			// Assert
			Assert.IsNotNull(states);
			Assert.AreEqual(50, states.Count);
			states.ForEach(s =>
			{
				Assert.IsFalse(s.LinkedStates.IsNullOrEmpty());
				Assert.AreEqual(4, s.LinkedStates.Count);
			});
		}
	}
}
