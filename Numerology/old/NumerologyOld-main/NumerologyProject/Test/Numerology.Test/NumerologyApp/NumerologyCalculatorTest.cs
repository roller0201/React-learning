using Numerology.Application.Init;
using Numerology.Application.Interfaces;
using Numerology.Application.Services;
using Numerology.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;

namespace Numerology.Test.NumerologyApp
{
    public class NumerologyCalculatorTest
    {
        private readonly INumerologyCalculator _numerologyCalculator;
        private IList<LetterModel> Letters { get; set; }
        public NumerologyCalculatorTest()
        {
            this._numerologyCalculator = new NumerologyCalculator();
            //System.Diagnostics.Debugger.Launch();
            this.Letters = LetterDBInit.BaseLetterList;
        }

        [Fact]
        public async void CheckCalculationForMainNumberFromBirthDate()
        {
            var response = await this._numerologyCalculator.CalculateNumerologyBirthDate(new DateTime(1996, 4, 11));

            //Assert.True(false);
            Assert.Equal("4", response[0]);
        }

        [Fact]
        public async void CheckCalculationForSubNumberFromBirthDate()
        {
            var response = await this._numerologyCalculator.CalculateNumerologyBirthDate(new DateTime(1996, 4, 11));

            Assert.Equal("31", response[1]);
        }

        [Fact]
        public async void CheckCalculationForMainNumberFromBirthDateString()
        {
            var response = await this._numerologyCalculator.CalculateNumerologyBirthDate("19960411");

            Assert.Equal("4", response[0]);
        }

        [Fact]
        public async void CheckCalculationForSubNumberFromBirthDateString()
        {
            var response = await this._numerologyCalculator.CalculateNumerologyBirthDate("19960411");

            Assert.Equal("31", response[1]);
        }

        [Fact]
        public async void Later()
        {
            var response = await this._numerologyCalculator.CalculateInsideNumber("Adrian Eryk Krysiak", this.Letters);

            Assert.Equal("8", response);
        }

        [Fact]
        public async void Later2()
        {
            var response = await this._numerologyCalculator.CalculateOutsideNumber("Adrian Eryk Krysiak", this.Letters);

            Assert.Equal("3", response);
        }

        [Fact]
        public async void CalculateUpRow()
        {
            var names = "Adrian Eryk Krysiak";

            //var letters = await letterService.GetList(x => x.Id > 0);
            var upRowMainWhole = await this._numerologyCalculator.CalculateInsideNumber(names, this.Letters);
            var downRowMainWhole = await this._numerologyCalculator.CalculateOutsideNumber(names, this.Letters);
            var upRow = await this._numerologyCalculator.CalculateNameUpRow(names, this.Letters);
            var upRowMain = await this._numerologyCalculator.CalculateNameMainRow(upRow.Split('-'));
            var downRow = await this._numerologyCalculator.CalculateNameDownRow(names, this.Letters);
            var downRowMain = await this._numerologyCalculator.CalculateNameMainRow(downRow.Split('-'));


            Assert.Equal("8", upRowMainWhole);
            Assert.Equal("3", downRowMainWhole);
            Assert.Equal("1  91 -5   -    91 -", upRow);
            Assert.Equal("   2     5     1    ", upRowMain);
            Assert.Equal(" 49  5- 972-2971  2-", downRow);
            Assert.Equal("   9     9     3    ", downRowMain);
        }

        [Fact]
        public async void CalculateEachNumber()
        {
            var names = "Adrian Eryk Krysiak";

            var test = await this._numerologyCalculator.CalculateEachNumber(names, this.Letters);
            var one = test["1"];
            var two = test["2"];
            var three = test["3"];
            var four = test["4"];
            var five = test["5"];
            var six = test["6"];
            var seven = test["7"];
            var eight = test["8"];
            var nine = test["9"];

            Assert.Equal("4", one);
            Assert.Equal("3", two);
            Assert.Equal("0", three);
            Assert.Equal("1", four);
            Assert.Equal("2", five);
            Assert.Equal("0", six);
            Assert.Equal("2", seven);
            Assert.Equal("0", eight);
            Assert.Equal("5", nine);
        }

        [Fact]
        public async void CheckMainNumbers()
        {
            string name = "Adrian Eryk Krysiak Adii Daniel Juliusz Klaudiusz Ink";
            var upRowMainWhole = await this._numerologyCalculator.CalculateInsideNumber(name, this.Letters);
            var downRowMainWhole = await this._numerologyCalculator.CalculateOutsideNumber(name, this.Letters);
            var upRow = await this._numerologyCalculator.CalculateNameUpRow(name, this.Letters);
            var upRowMain = await this._numerologyCalculator.CalculateNameMainRow(upRow.Split('-'));
            var downRow = await this._numerologyCalculator.CalculateNameDownRow(name, this.Letters);
            var downRowMain = await this._numerologyCalculator.CalculateNameMainRow(downRow.Split('-'));

            var test2 = "";
        }

        [Fact]
        public async void CalculateBirthdateTree()
        {
            var test = await this._numerologyCalculator.CalculateTreeBirthDate("1996", "04", "11");


            Assert.Equal("6", test[0]);
            Assert.Equal("9", test[1]);
            Assert.Equal("6", test[2]);
            Assert.Equal("2", test[3]);
            Assert.Equal("2", test[4]);
            Assert.Equal("5", test[5]);
            Assert.Equal("3", test[6]);
            Assert.Equal("3", test[7]);
        }

        [Fact]
        public async void CalculateBirthdateTree2()
        {
            var test = await this._numerologyCalculator.CalculateTreeBirthDate("1990", "01", "09");


            var test2 = "";
        }
    }
}
