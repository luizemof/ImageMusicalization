using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Common.Extraction;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ext = Extraction.KMeans;

namespace Extraction.Tests.KMeans
{
	[TestClass]
	public class KMeansTests
	{
		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void KMeans_Execute_Empty_Image()
		{
			// Arrange
			List<Point> points = new List<Point>()
			{
				new Point(0, 0),
				new Point(10, 10)
			};
			Ext.KMeans kMeans = new Ext.KMeans(
				myImageFile: string.Empty,
				initalCenters: points,
				parallel: true);

			// Act
			kMeans.Execute();
		}

		[TestMethod]
		[ExpectedException(typeof(NotImplementedException))]
		public void KMeans_Execute_Empty_Points_Random()
		{
			// Arrange
			Ext.KMeans kMeans = new Ext.KMeans(
				myImageFile: Constants.ImageFile,
				initalCenters: new List<Point>(),
				parallel: true);

			// Act
			kMeans.Execute();
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentOutOfRangeException))]
		public void KMeans_Execute_With_Center_Bigger_Than_Image()
		{
			// Arrange

			List<Point> points = new List<Point>()
			{
				new Point(10, 15),
				new Point(20, 25),
				new Point(30, 35),
				new Point(40, 45),
				new Point(50, 55),
				new Point(60, 65),
				new Point(70, 75),
			};
			Ext.KMeans kMeans = new Ext.KMeans(
				myImageFile: Constants.ImageFile,
				initalCenters: points,
				parallel: true);

			// Act
			kMeans.Execute();
		}

		[TestMethod]
		public void KMeans_Execute_With_One_Center()
		{
			// Arrange
			List<Point> points = new List<Point>() { new Point(10, 10) };
			Ext.KMeans kMeans = new Ext.KMeans(
				myImageFile: Constants.ImageFile,
				initalCenters: points,
				parallel: true);

			// Act
			kMeans.Execute();

			// Assert
			Assert.IsFalse(kMeans.Result.IsNullOrEmpty());
			Assert.AreEqual(1, kMeans.Result.Count);
			Assert.AreEqual(6, kMeans.Result[0].Coordinator.X);
			Assert.AreEqual(27, kMeans.Result[0].Coordinator.Y);
			Assert.AreEqual(ENote.Dm, kMeans.Result[0].Note);
			Assert.AreEqual(5600, kMeans.Result[0].NumberOfElements);
		}

		[TestMethod]
		public void KMeans_Execute_With_Seven_Center()
		{
			// Arrange
			List<IResult> expectedResults = new List<IResult>()
			{
				new Ext.KMeansResult()
				{
					Coordinator = new Point(12, 14),
					Note = ENote.C,
					NumberOfElements = 489
				},
				new Ext.KMeansResult()
				{
					Coordinator = new Point(24, 28),
					Note = ENote.F,
					NumberOfElements = 568
				},
				new Ext.KMeansResult()
				{
					Coordinator = new Point(33, 36),
					Note = ENote.Dm,
					NumberOfElements = 649
				},
				new Ext.KMeansResult()
				{
					Coordinator = new Point(38, 21),
					Note = ENote.Dm,
					NumberOfElements = 869
				},
				new Ext.KMeansResult()
				{
					Coordinator = new Point(55, 31),
					Note = ENote.F,
					NumberOfElements = 872
				},
				new Ext.KMeansResult()
				{
					Coordinator = new Point(43, 3),
					Note = ENote.Dm,
					NumberOfElements = 1024
				},
				new Ext.KMeansResult()
				{
					Coordinator = new Point(62, 10),
					Note = ENote.C,
					NumberOfElements = 1129
				}
			};
			List<Point> points = new List<Point>()
			{
				new Point(10, 15),
				new Point(20, 25),
				new Point(30, 27),
				new Point(40, 30),
				new Point(50, 35),
				new Point(60, 45),
				new Point(70, 55),
			};

			Ext.KMeans kMeans = new Ext.KMeans(
				myImageFile: Constants.ImageFile,
				initalCenters: points,
				parallel: true);

			// Act
			kMeans.Execute();

			// Assert
			Assert.IsFalse(kMeans.Result.IsNullOrEmpty());
			Assert.AreEqual(expectedResults.Count, kMeans.Result.Count);

			List<IResult> orderedList = kMeans.Result.OrderBy(r => r.NumberOfElements).ToList();
			for (int i = 0; i < kMeans.Result.Count; i++)
			{
				Assert.AreEqual(expectedResults[i].Coordinator.X, orderedList[i].Coordinator.X);
				Assert.AreEqual(expectedResults[i].Coordinator.Y, orderedList[i].Coordinator.Y);
				Assert.AreEqual(expectedResults[i].Note, orderedList[i].Note);
				Assert.AreEqual(expectedResults[i].NumberOfElements, orderedList[i].NumberOfElements);
			}
		}
	}
}
