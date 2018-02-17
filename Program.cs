using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Virtual_Cell
{
    class Program
    {

        static void Main(string[] args)
        {
            //Tests


            //Constants
                //in cubic meters
            double cellVolume = .000001;
            double habVolume = 1;

            double sub1ConcentrationConstant = 500;
            double sub1TemperatureConstant = 350;
                //in moles
            double sub1CellInitQuantity = .0015;

            double sub2ConcentrationConstant = 150;
            double sub2TemperatureConstant = 30;
                //in moles
            double sub2CellInitQuantity = .0008;

            double prod1ConcentrationConstant = 300;
            double prod1TemperatureConstant = 180;
                //in moles
            double prod1CellInitQuantity = 0;

            double prod2ConcentrationConstant = 380;
            double prod2TemperatureConstant = 65;
                //in moles
            double prod2CellInitQuantity = 0;

            double enz1FrwrdRate = 16000;
            double enz1RvrsRate = 950;
            double enz1CatRate = 9700;
                //in moles
            double enz1InitQuantity = .00000175;

            double enz2FrwrdRate = 920;
            double enz2RvrsRate = 105;
            double enz2CatRate = 6400;
                //in moles
            double enz2InitQuantity = .00000257;

            //Initiating particle/enzyme lists for the cell and its habitat
            List<Particle> cellParts = new List<Particle>();
            List<Particle> habParts = new List<Particle>();

            List<Enzyme> cellEnzs = new List<Enzyme>();
            List<Enzyme> habEnzs = new List<Enzyme>();

            //Creating the particle structures
            Particle sub1Cell = new Particle(ChemicalSpecies.Substrate1,sub1ConcentrationConstant, sub1TemperatureConstant,
                                             sub1CellInitQuantity, cellVolume);
            Particle sub2Cell = new Particle(ChemicalSpecies.Substrate2, sub2ConcentrationConstant, sub2TemperatureConstant,
                                             sub2CellInitQuantity, cellVolume);

            Particle sub1Hab = new Particle(ChemicalSpecies.Substrate1, sub1ConcentrationConstant, sub1TemperatureConstant,
                                             sub1CellInitQuantity, cellVolume);
            Particle sub2Hab = new Particle(ChemicalSpecies.Substrate2, sub2ConcentrationConstant, sub2TemperatureConstant,
                                             sub2CellInitQuantity, cellVolume);

            Particle prod1Cell = new Particle(ChemicalSpecies.Product1, prod1ConcentrationConstant, prod1TemperatureConstant,
                                             prod1CellInitQuantity, cellVolume);
            Particle prod2Cell = new Particle(ChemicalSpecies.Product2, prod2ConcentrationConstant, prod2TemperatureConstant,
                                             prod2CellInitQuantity, cellVolume);

            Particle prod1Hab = new Particle(ChemicalSpecies.Product1, prod1ConcentrationConstant, prod1TemperatureConstant,
                                             prod1CellInitQuantity, habVolume);
            Particle prod2Hab = new Particle(ChemicalSpecies.Product2, prod2ConcentrationConstant, prod2TemperatureConstant,
                                             prod2CellInitQuantity, habVolume);

            //Creating the enzyme structures
            Enzyme enzyme1 = new Enzyme(ChemicalSpecies.Substrate1, ChemicalSpecies.Product1, enz1FrwrdRate, enz1RvrsRate,
                                        enz1CatRate, enz1InitQuantity,cellVolume);
            Enzyme enzyme2 = new Enzyme(ChemicalSpecies.Substrate2, ChemicalSpecies.Product2, enz2FrwrdRate, enz2RvrsRate,
                                        enz2CatRate, enz2InitQuantity, cellVolume);

            //Creating Particle and Enzyme Lists
            cellParts.Add(sub1Cell);
            cellParts.Add(sub2Cell);
            cellParts.Add(prod1Cell);
            cellParts.Add(prod2Cell);

            cellEnzs.Add(enzyme1);
            cellEnzs.Add(enzyme2);

            habParts.Add(sub1Hab);
            habParts.Add(sub2Hab);
            habParts.Add(prod1Hab);
            habParts.Add(prod2Hab);


            //Creating the cell and its "habitat"
            Cell VC = new Cell(cellParts,cellEnzs,cellVolume, 27);

            List<Cell> habCells = new List<Cell>();
            habCells.Add(VC);

            Habitat hab = new Habitat(habParts,habCells, 27, habVolume);

            //Run simulation/"live"/update
            hab.WriteCurrentState();
            hab.Update(20, 1);
            hab.WriteCurrentState();

            Console.ReadKey();

        }
    }
}
