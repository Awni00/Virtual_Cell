using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Virtual_Cell
{
    /// <summary>
    /// A domain where cells and particles exist
    /// </summary>
    public class Habitat
    {
        //the set of chemical species in the habitat
        List<Particle> particles = new List<Particle>();

        //the set of cells in the habitat
        List<Cell> cells = new List<Cell>();


        //temerature of habitat (exterior of cell)
        double temperature;

        //habitat volume
        double volume;

        /// <summary>
        /// habitat constructor
        /// </summary>
        public Habitat(List<Particle> particles, List<Cell> cells, double initTemp, double volume)
        {
            this.particles = particles;
            this.cells = cells;
            this.temperature = initTemp;
            this.volume = volume;
        }

        public List<Particle> Particles
        {
            get { return particles; }
        }

        public List<Cell> Cells
        {
            get { return cells; }
        }

        public double Temperature
        {
            get { return temperature; }
        }

        public double Volume
        {
            get { return volume; }
        }

        /// <summary>
        /// Updates state of all cells in habitat, as well as habitat's own state
        /// </summary>
        /// <returns>void</returns>
        /// <param name="updateCount">the number of times to update</param>
        /// <param name="updateInterval">the length of an update iteration(delta t)</param>
        public void Update(int updateCount, double updateInterval)
        {
            double runTime = updateCount * updateInterval;

            // update updateCount times
            for (int i = 0; i < updateCount; i++)
            {
                // update each cell in habitat(cell's update function also updates habitat's particle count)
                    // cell update function also updates its state records
                foreach (Cell cell in this.cells)
                {
                    cell.Update(updateInterval, this);
                }

                // update records of habitat's particles
                foreach (var habParticle in this.particles)
                {
                    habParticle.UpdateRecords();
                }
            }

        }

        public void WriteCurrentState()
        {
            Console.WriteLine("------Habitat's Current State:");

            Console.WriteLine("--Habitat Particles:");
            foreach (var particle in this.particles)
            {
                particle.WriteCurrentState();
            }

            Console.WriteLine("--Cells' States:");
            foreach (var cell in cells)
            {
                cell.WriteCurrentState();
            }

        }

    }
}
