﻿/*
 * Copyright (c) 2011 Stephen A. Pratt
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
using System;
using System.Runtime.InteropServices;

namespace org.critterai.nav
{
    /// <summary>
    /// Configuration parameters for <see cref="CrowdAgent"/> objects.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CrowdAgentParams
    {
        /*
         * Source: DetourCrowd.h dtCrowdAgentParams (struct)
         * 
         * Design notes:
         * 
         * Structure vs Class
         * 
         * I'm still uncertain whether this should be implemented as
         * a class or a structure.  But I've chosen structure for now.
         * 
         * I will need a structure in order to support passing 
         * the data as an interop array. An array will be required if I want
         * to support more efficent mass updates.  (Add/update multiple agents
         * at once.)
         * 
         * The significant downside is that a structure doesn't support 
         * Unity serialization.
         * 
         */

        /// <summary>
        /// Agent radius. [Limit: >= 0]
        /// </summary>
	    public float radius;

        /// <summary>
        /// Agent height. [Limit: > 0]
        /// </summary>
        public float height;

        /// <summary>
        /// Maximum allowed acceleration. [Limit: >= 0]
        /// </summary>
        public float maxAcceleration;

        /// <summary>
        /// Maximum allowed speed. [Limit: >= 0]
        /// </summary>
        public float maxSpeed;

        /// <summary>
        /// Defines how close a neighbor must be before it is considered
        /// in steering behaviors. [Limit: > 0]
        /// </summary>
        /// <remarks>
        /// <p>The value is often based on the agent radius and/or
        /// and maximum speed.  E.g. radius * 8</p></remarks>
        public float collisionQueryRange;

        /// <summary>
        /// The path optimization range.
        /// </summary>
        /// <remarks>
        /// <p>This value is often based on the agent radius. E.g. radius * 30
        /// </p>
        /// </remarks>
        public float pathOptimizationRange;

        /// <summary>
        /// How aggresive the agent manager should be at avoiding
        /// collisions with this agent.
        /// </summary>
        /// <remarks>
        /// <p>A higher value will result in agents trying to stay farther away 
        /// from eachother at the cost of more difficult steering in tight
        /// spaces.</p></remarks>
        public float separationWeight;

        /// <summary>
        /// Flags that impact steering behavior.
        /// </summary>
        public CrowdUpdateFlags updateFlags;

        /// <summary>
        /// The index of the avoidance parameters to use for the agent.
        /// </summary>
        /// <remarks>
        /// <p>The <see cref="CrowdManager"/> permits agents to use different
        /// avoidance configurations.  (See 
        /// <see cref="CrowdManager.SetAvoidanceConfig"/>.)  This value
        /// is the index of the configuration to use.</p>
        /// </remarks>
        /// <seealso cref="CrowdAvoidanceParams"/>
        public byte avoidanceType;

        // Must exist for marshalling.  Not used on managed side of boundary.
        // On the native side this is a void pointer for custom user data.
	    private IntPtr userData;

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="config">The configuration to copy.</param>
        public CrowdAgentParams(CrowdAgentParams config)
        {
            this.radius = config.radius;
            this.height = config.height;
            this.maxAcceleration = config.maxAcceleration;
            this.maxSpeed = config.maxSpeed;
            this.collisionQueryRange = config.collisionQueryRange;
            this.pathOptimizationRange = config.pathOptimizationRange;
            this.separationWeight = config.separationWeight;
            this.updateFlags = config.updateFlags;
            this.avoidanceType = config.avoidanceType;
            this.userData = config.userData;
        }
    }
}
