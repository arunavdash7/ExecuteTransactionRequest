protected override void ExecuteCrmPlugin(LocalPluginContext localContext)
        {
            if (localContext == null)
            {
                throw new ArgumentNullException("localContext");
            }

            // TODO: Implement your custom plug-in business logic.
            IOrganizationService service = localContext.OrganizationService;
            IExecutionContext context = localContext.PluginExecutionContext;
            if (context.InputParameters.Contains("Target") &&
                context.InputParameters["Target"] is Entity)
            {
                try
                {
                    //Incident incident = (context.InputParameters["Target"] as Entity).ToEntity<Incident>();
                    Entity order = (Entity)context.InputParameters["Target"];

                    if (order == null)
                    {
                        throw new ArgumentNullException("Entities", new Exception("Entities Collection not found to create"));
                    }
                    else
                    {
                        throw new InvalidPluginExecutionException("Exception");
                    }
                }
                catch (Exception ex)
                {
                    throw new InvalidPluginExecutionException(ex.Message);
                }
            }
        }
