using UnityEngine;

namespace SerapKeremZenCoreTools._Game.PopUpSystem
{
    /// <summary>
    /// Defines the types of animations available for pop-ups.
    /// </summary>
    public enum PopUpAnimationType
    {
        /// <summary>
        /// Scales the pop-up and fades it out over time.
        /// </summary>
        [InspectorName("Scale and Fade")]
        ScaleAndFade,

        /// <summary>
        /// Slides the pop-up upwards and fades it out.
        /// </summary>
        [InspectorName("Slide Up")]
        SlideUp,

        /// <summary>
        /// Slides the pop-up downwards and fades it out.
        /// </summary>
        [InspectorName("Slide Down")]
        SlideDown,

        /// <summary>
        /// Applies a bouncing effect to the pop-up.
        /// </summary>
        [InspectorName("Bounce")]
        Bounce
    }
}