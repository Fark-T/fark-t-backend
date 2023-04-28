namespace fark_t_backend.Provider
{
    public interface IHttpContextProvider
    {
        public Guid GetCurrentUser();
    }
}
