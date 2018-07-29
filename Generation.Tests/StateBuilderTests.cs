using System;
using System.Collections.Generic;
using System.Drawing;
using Common;
using Common.Extraction;
using Common.Generation;
using Extraction.KMeans;
using Extraction.SURF;
using Generation.KMeans;
using Generation.SURF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Generation.Tests
{
	[TestClass]
	public class StateBuilderTests
	{
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void StateBuilder_Without_Result_ArgumentException()
		{
			StatesBuilder stateBuilder = new StatesBuilder(null, EModelType.SURF);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void StateBuilder_Without_Valid_Type_ArgumentException()
		{
			// Arrange
			StatesBuilder stateBuilder = new StatesBuilder(new List<IResult>(), EModelType.SURF);
		}

		[TestMethod]
		public void StateBuilder_SURFType()
		{
			// Arrange
			SURFResult result = new SURFResult()
			{
				Coordinator = new Point(1, 1),
				Pixel = Color.Red,
				Note = ENote.Am,
				NumberOfElements = 12
			};

			StatesBuilder stateBuilder = new StatesBuilder(new List<IResult>() { result }, EModelType.SURF);

			// Act
			List<IState> states = stateBuilder.Build();

			// Arrange
			Assert.IsFalse(states.IsNullOrEmpty());
			states.ForEach(s =>
			{
				Assert.IsTrue(s.ModelType == EModelType.SURF);
				Assert.IsInstanceOfType(s, typeof(SURFState));
				Assert.IsNotNull(((SURFState)s).Element);
			});
		}

		public void StateBuilder_KMeansType()
		{
			// Arrange
			KMeansResult result = new KMeansResult()
			{
				Coordinator = new Point(1, 1),
				Note = ENote.C,
				NumberOfElements = 100,
				Pixel = Color.Red
			};

			StatesBuilder stateBuilder = new StatesBuilder(new List<IResult>() { result }, EModelType.KMeans);

			// Act
			List<IState> states = stateBuilder.Build();

			// Arrange
			Assert.IsFalse(states.IsNullOrEmpty());
			states.ForEach(s =>
			{
				Assert.IsTrue(s.ModelType == EModelType.KMeans);
				Assert.IsInstanceOfType(s, typeof(KMeansState));
				Assert.IsNotNull(((KMeansState)s).Element);
			});
		}
	}
}
