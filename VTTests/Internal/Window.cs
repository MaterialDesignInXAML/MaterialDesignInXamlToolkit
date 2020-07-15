namespace VTTests.Internal
{
    internal class Window : VisualElement, IWindow
    {
        public Window(Protocol.ProtocolClient client, string id)
            : base(client, id)
        { }

        protected override ElementQuery GetFindElementQuery(string query)
            => new ElementQuery
            {
                WindowId = Id,
                Query = query
            };
    }
}
