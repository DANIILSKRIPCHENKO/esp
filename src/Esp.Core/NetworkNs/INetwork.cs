﻿using Esp.Core.Common;

namespace Esp.Core.NetworkNs
{
    /// <summary>
    /// Represents interface for Neural Network
    /// </summary>
    public interface INetwork : IId
    {
        /// <summary>
        /// Calculates and applies fitness to neurons
        /// </summary>
        /// <returns></returns>
        public double ApplyFitness();

        /// <summary>
        /// Reset connections of neurons
        /// </summary>
        public void ResetConnection();

        /// <summary>
        /// Pushes expected outpur values in Network
        /// </summary>
        /// <param name="expectedOutputs"></param>
        public void PushExpectedValues(IList<double> expectedOutputs)
    }
}
