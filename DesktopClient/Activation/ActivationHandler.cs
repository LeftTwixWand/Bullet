using System.Threading.Tasks;

namespace DesktopClient.Activation
{
    /// <summary>
    /// The ActivationService is in charge of handling the applications initialization and activation.
    /// </summary>
    // For more information on understanding and extending activation flow see
    // https://github.com/microsoft/WindowsTemplateStudio/blob/master/docs/UWP/activation.md
    internal abstract class ActivationHandler
    {
        /// <summary>
        /// CanHandle checks the args is of type you have configured
        /// </summary>
        /// <param name="args">Activation service object</param>
        /// <returns>True if it's possible to handle the service</returns>
        internal abstract bool CanHandle(object activationArgs);

        /// <summary>
        /// Handle the service
        /// </summary>
        /// <param name="args">Activation service object</param>
        /// <returns>Completed task</returns>
        internal abstract Task HandleAsync(object activationArgs);
    }

    /// <summary>
    /// Extend this class to implement new ActivationHandlers
    /// </summary>
    /// <typeparam name="T">Any activation service</typeparam>
    internal abstract class ActivationHandler<T> : ActivationHandler where T : class
    {
        /// <summary>
        /// Override this method to add the activation logic in your activation handler
        /// </summary>
        /// <param name="args">Any activation service</param>
        /// <returns></returns>
        protected abstract Task HandleInternalAsync(T args);

        /// <summary>
        /// Handle the service
        /// </summary>
        /// <param name="args">Activation service object</param>
        /// <returns>Completed task</returns>
        internal override async Task HandleAsync(object args) => await HandleInternalAsync(args as T);

        /// <summary>
        /// CanHandle checks the args is of type you have configured
        /// </summary>
        /// <param name="args">Activation service object</param>
        /// <returns>True if it's possible to handle the service</returns>
        internal override bool CanHandle(object args) => args is T && CanHandleInternal(args as T);

        // You can override this method to add extra validation on activation args
        // to determine if your ActivationHandler should handle this activation args
        protected virtual bool CanHandleInternal(T args)
        {
            return true;
        }
    }
}