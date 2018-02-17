using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Virtual_Cell
{
    /// <summary>
    /// A structure which processes particles of a particular chemical species 
    /// </summary>
    public class Particle
    {
        //the quantity of this particle in this domain
        double quantity;

        //a record of this particle's quantity in its domain
        List<double> quantityRecord = new List<double>();

        //concentration in domain
        double concentration;

        // a record of this particle's concentration in its domain
        List<double> concentrationRecord = new List<double>();

        //a constant weighing the influence of the concentration gradient on the particle's diffusion
        double concentrationConstant;

        //a constant weighing the influence of the temperature gradient on the particle's diffusion
        double temperatureConstant;

        //the particle's chemical species
        ChemicalSpecies chemicalSpecies;

        public ChemicalSpecies ChemicalSpecies
        {
            get { return chemicalSpecies; }
        }

        public double Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public double Concentration
        {
            get { return concentration; }
        }

        public double ConcentrationConstant
        {
            get { return concentrationConstant; }
        }

        public double TemperatureConstant
        {
            get { return temperatureConstant; }
        }

        // record of particle's quantity in its domain
        public List<double> QuantityRecord
        {
            get { return quantityRecord; }
        }

        // record of particle's concentration in its domain
        public List<double> ConcentrationRecord
        {
            get { return concentrationRecord; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Virtual_Cell.Particle"/> class.
        /// </summary>
        /// <param name="chemicalSpecies">Chemical species.</param>
        /// <param name="concentrationConstant">Concentration constant.</param>
        /// <param name="temperatureConstant">Temperature constant.</param>
        /// <param name="initQuantity">Init quantity.</param>
        /// <param name="domainVolume">Domain volume.</param>
        public Particle(ChemicalSpecies chemicalSpecies, double concentrationConstant, double temperatureConstant, double initQuantity, double domainVolume)
        {
            this.chemicalSpecies = chemicalSpecies;

            this.concentrationConstant = concentrationConstant;
            this.temperatureConstant = temperatureConstant;

            this.quantity = initQuantity;
            this.concentration = quantity / domainVolume;
        }


        /// <summary>
        /// Updates the concentration of this particle in its domain
        /// </summary>
        /// <param name="domainVolume">Domain volume.</param>
        public void UpdateConcentration(double domainVolume)
        {
            concentration = quantity / domainVolume;
        }

        /// <summary>
        /// Updates the records of the particle's state with its current state
        /// </summary>
        public void UpdateRecords()
        {
            this.quantityRecord.Add(this.quantity);
            this.concentrationRecord.Add(this.Concentration);
        }

        /// <summary>
        /// Calculates the reaction derivative of the product's concentration
        /// </summary>
        /// <returns>the derivative of the product's concentration due to this reaction</returns>
        /// <param name="Vmax">Enzyme's maximum reaction rate</param>
        /// <param name="Kd">Enzyme's dissociation constant</param>
        /// <param name="substrateConcentration">the concentration of substrate which is being converted to this product</param>
        static public double CalculateReactionDerivative(double Vmax, double Kd, double substrateConcentration)
        {
            return (Vmax * substrateConcentration) / (Kd + substrateConcentration);
        }

        /// <summary>
        /// Writes the current state of the particle
        /// </summary>
        public void WriteCurrentState()
        {
            Console.WriteLine("-");
            Console.WriteLine("Quantity: " + this.quantity);
            Console.WriteLine("Concentration: " + this.concentration);
        }
    }
}
