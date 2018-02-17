using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Virtual_Cell
{
    /// <summary>
    /// A biological catalyst which converts substrate species into product species
    /// </summary>
    public class Enzyme
    {
        //the chemcial species of the substrate the enzyme acts on
        ChemicalSpecies substrateSpecies;

        //the chemical species of the product the enzyme produces
        ChemicalSpecies productSpecies;

        //Total quantity of enzyme
        double totalQuantity;

        //current quantity of unbound/active enzyme in domain
        double quantity;

        //record of enzyme's unbound quantity in domain
        List<double> quantityRecord = new List<double>();

        //concentration of unbound/active enzyme in domain
        double concentration;

        //record of enzyme's unbound concentration 
        List<double> concentrationRecord = new List<double>();

        //forward reaction rate (forward to complex)
        double frwrdRate;

        //reverse reaction rate (reverse from complex)
        double rvrsRate;

        //catalysis rate (to product)
        double catRate;

        //Maximum reaction rate
        double maxReactionRate;

        //dissociation constate
        double dissociationConstant;

        //enzyme-substrate complex concentration
        double complexConcentration = 0;

        //record of complex concentration
        List<double> complexConcentrationRecord = new List<double>();

        /// <summary>
        /// Enzyme constructor
        /// </summary>
        public Enzyme(ChemicalSpecies substrateSpecies, ChemicalSpecies productSpecies, double frwrdRate, double rvrsRate, double catRate, double initQuantity, double domainVolume)
        {
            this.substrateSpecies = substrateSpecies;
            this.productSpecies = productSpecies;

            this.frwrdRate = frwrdRate;
            this.rvrsRate = rvrsRate;
            this.catRate = catRate;

            this.quantity = initQuantity;
            this.totalQuantity = initQuantity;
            this.concentration = quantity / domainVolume;

            this.dissociationConstant = rvrsRate / frwrdRate;
            this.maxReactionRate = catRate * totalQuantity;
        }

        public ChemicalSpecies SubstrateSpecies
        {
            get { return substrateSpecies; }
        }

        public ChemicalSpecies ProductSpecies
        {
            get { return productSpecies; }
        }

        public double Concentration
        {
            get { return concentration; }
        }

        public double Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public double FrwrdRate
        {
            get { return frwrdRate; }
        }

        public double RvrsRate
        {
            get { return rvrsRate; }
        }

        public double CatRate
        {
            get { return catRate; }
        }

        //gets enzyme's maximum reaction rate (Vmax)
        public double MaximumReactionRate
        {
            get { return maxReactionRate; }
        }

        //gets enzyme's dissociation constant
        public double DissociationConstant
        {
            get { return dissociationConstant; }
        }

        //get's enzyme's current complex concentration
        public double ComplexConcentration
        {
            get { return complexConcentration; }
        }

        // record of enzyme's quantity in its domain
        public List<double> QuantityRecord
        {
            get { return quantityRecord; }
        }

        // record of enzyme's concentration in its domain
        public List<double> ConcentrationRecord
        {
            get { return concentrationRecord; }
        }

        // record of the enzyme complex concentration in its domain
        public List<double> ComplexConcentrationRecord
        {
            get { return complexConcentrationRecord; }
        }

        /// <summary>
        /// Updates the concentration.
        /// </summary>
        /// <param name="domainVolume">Domain volume.</param>
        public void UpdateConcentration(double domainVolume)
        {
            concentration = quantity / domainVolume;
        }

        /// <summary>
        /// Updates the complex concentration.
        /// </summary>
        /// <param name="substrateConcentration">Substrate concentration.</param>
        public void UpdateComplexConcentration(double substrateConcentration)
        {
            complexConcentration = (totalQuantity * substrateConcentration) / (dissociationConstant + substrateConcentration);
        }

        /// <summary>
        /// Updates the records of the enzyme's state with its current state
        /// </summary>
        public void UpdateRecords()
        {
            this.quantityRecord.Add(this.quantity);
            this.concentrationRecord.Add(this.concentration);

            this.complexConcentrationRecord.Add(this.complexConcentration);
        }

        public void WriteCurrentState()
        {
            Console.WriteLine("-");
            Console.WriteLine("Quantity: " + quantity);
            Console.WriteLine("Active/Unbound Concentration: " + concentration);
            Console.WriteLine("Complex Concentration: " + complexConcentration);
        }

    }

}
