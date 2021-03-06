//////__     __               _                 _     _               _   
//////\ \   / /              | |               | |   | |             | |  
////// \ \_/ /__  _   _   ___| |__   ___  _   _| | __| |  _ __   ___ | |_ 
//////  \   / _ \| | | | / __| '_ \ / _ \| | | | |/ _` | | '_ \ / _ \| __|
//////   | | (_) | |_| | \__ \ | | | (_) | |_| | | (_| | | | | | (_) | |_ 
//////   |_|\___/ \__,_| |___/_| |_|\___/ \__,_|_|\__,_| |_| |_|\___/ \__|
//////      _                              _   _     _     _ 
//////     | |                            | | | |   (_)   | |
//////  ___| |__   __ _ _ __   __ _  ___  | |_| |__  _ ___| |
////// / __| '_ \ / _` | '_ \ / _` |/ _ \ | __| '_ \| / __| |
//////| (__| | | | (_| | | | | (_| |  __/ | |_| | | | \__ \_|
////// \___|_| |_|\__,_|_| |_|\__, |\___|  \__|_| |_|_|___(_)
//////                         __/ |                         
//////                        |___/                          

using System;
using Project290.Physics.Dynamics;

namespace Project290.Physics.Common.PhysicsLogic
{
    [Flags]
    public enum PhysicsLogicType
    {
        Explosion = (1 << 0)
    }

    public class FilterPhysicsLogicData : FilterData
    {
        private PhysicsLogicType _type;

        public FilterPhysicsLogicData(PhysicsLogicType type)
        {
            _type = type;
        }

        public override bool IsActiveOn(Body body)
        {
            if (body.PhysicsLogicFilter.IsPhysicsLogicIgnored(_type))
                return false;

            return base.IsActiveOn(body);
        }
    }

    public class PhysicsLogicFilter
    {
        public PhysicsLogicType ControllerIgnores;

        /// <summary>
        /// Ignores the controller. The controller has no effect on this body.
        /// </summary>
        /// <param name="type">The logic type.</param>
        public void IgnorePhysicsLogic(PhysicsLogicType type)
        {
            ControllerIgnores |= type;
        }

        /// <summary>
        /// Restore the controller. The controller affects this body.
        /// </summary>
        /// <param name="type">The logic type.</param>
        public void RestorePhysicsLogic(PhysicsLogicType type)
        {
            ControllerIgnores &= ~type;
        }

        /// <summary>
        /// Determines whether this body ignores the the specified controller.
        /// </summary>
        /// <param name="type">The logic type.</param>
        /// <returns>
        /// 	<c>true</c> if the body has the specified flag; otherwise, <c>false</c>.
        /// </returns>
        public bool IsPhysicsLogicIgnored(PhysicsLogicType type)
        {
            return (ControllerIgnores & type) == type;
        }
    }

    public abstract class PhysicsLogic
    {
        public FilterPhysicsLogicData FilterData;
        public World World;

        public PhysicsLogic(World world, PhysicsLogicType type)
        {
            FilterData = new FilterPhysicsLogicData(type);
            World = world;
        }
    }
}