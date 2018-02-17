using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Virtual_Cell
{
    /// <summary>
    /// A virtual biological cell which carries out metabolism
    /// </summary>
    public class Cell
    {
        //the set of chemcial species present in the cell
        List<Particle> particles = new List<Particle>();

        //the set of enzmes present in the cell
        List<Enzyme> enzymes = new List<Enzyme>();

        //Cell's diameter
        double diameter;

        //cell's surface area
        double surfaceArea;

        //cell's volume
        double volume;

        //Cell's temperature
        double temperature;


        /// <summary>
        /// C constructor
        /// </summary>
        public Cell(List<Particle> particles, List<Enzyme> enzymes, double initVolume, double initTemperature)
        {
            this.particles = particles;
            this.enzymes = enzymes;

            this.volume = initVolume;
            this.diameter = Math.Pow((6 / Math.PI) * volume, 1.0 / 3);
            this.surfaceArea = Math.PI * 4 * Math.Pow((diameter / 2.0),2);

            this.temperature = initTemperature;


        }

        public double Volume
        {
            get { return volume; }
            set { volume = value; }
        }

        public List<Particle> Particles
        {
            get { return particles; }
            set { particles = value; }
        }

        public List<Enzyme> Enzymes
        {
            get { return enzymes; }
            set { enzymes = value; }
        }

        /// <summary>
        /// Update cell (metabolize and update diffusion of particles)
        /// </summary>
        public void Update(double updateInterval, Habitat hab)
        {
            //diffusion
            foreach (Particle cellParticle in particles)
            {
                foreach (Particle habParticle in hab.Particles)
                {
                    if (cellParticle.ChemicalSpecies == habParticle.ChemicalSpecies)
                    {
                        double flowIntoCell = updateInterval * surfaceArea * ((cellParticle.ConcentrationConstant * (habParticle.Concentration - cellParticle.Concentration)) + (cellParticle.TemperatureConstant * (hab.Temperature - temperature)));
                        cellParticle.Quantity += flowIntoCell;
                        cellParticle.UpdateConcentration(this.volume);
                        habParticle.Quantity -= flowIntoCell;
                        habParticle.UpdateConcentration(hab.Volume);
                    }
                }
            }

            //enzymatic activity
            foreach (Particle substrate in this.particles)
            {

                foreach (Enzyme enzyme in this.enzymes)
                {
                    //check if this enzyme acts on this partile as a substrate
                    if (enzyme.SubstrateSpecies == substrate.ChemicalSpecies)
                    {
                        enzyme.UpdateConcentration(this.volume);
                        enzyme.UpdateComplexConcentration(substrate.Concentration);
                        foreach (Particle potentialProduct in particles)
                        {
                            if (potentialProduct.ChemicalSpecies == enzyme.ProductSpecies)
                            {
                                //update product's quantity based on derivative
                                potentialProduct.Quantity += this.volume * 
                                    Particle.CalculateReactionDerivative(enzyme.MaximumReactionRate, enzyme.DissociationConstant, substrate.Concentration) * updateInterval;

                                //update substrate's quantity based on derivative
                                substrate.Quantity += this.volume * 
                                    (-enzyme.FrwrdRate * enzyme.Concentration * substrate.Concentration
                                     + enzyme.RvrsRate * enzyme.ComplexConcentration) * updateInterval;

                                //update substrate's concentration
                                substrate.UpdateConcentration(this.volume);

                                //update enzyme's quantity based on derivative
                                enzyme.Quantity += this.volume * 
                                    (-enzyme.FrwrdRate * enzyme.Concentration * substrate.Concentration
                                     + enzyme.RvrsRate * enzyme.ComplexConcentration + 
                                     enzyme.CatRate * enzyme.ComplexConcentration) * updateInterval;
                            }
                        }
                    }
                }
            }

            //Update records of states
            foreach (Particle particle in this.particles)
            {
                particle.UpdateRecords();
            }

            foreach (Enzyme enzyme in this.enzymes)
            {
                enzyme.UpdateRecords();
            }

        }

        public void WriteCurrentState()
        {
            Console.WriteLine("------Cell's Current State:");

            Console.WriteLine("Temperature: " + this.temperature);

            Console.WriteLine("Volume: " + this.volume);

            Console.WriteLine("--Particles in Cell:");
            foreach (Particle particle in this.particles)
            {
                
                particle.WriteCurrentState();
            }
        }


    }

}
