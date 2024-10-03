// effortlessapiservices/BaseService.tsx
class BaseService {
    async apiCall(method :string = "GET", controller:string = "User", endpoint :string = "AppUsers", view :string|null = "Grid%20view", payload :Object|null, airtableWhere :string|null, params :string|null) {
    try {
        const token = localStorage.getItem("access_token");
  
        airtableWhere = airtableWhere ? `&airtableWhere=${airtableWhere}` : '';
        var url;
        view = view ? `&view=${view}` : '';
        if (params) {
            url = `https://localhost:42123/${controller}/${endpoint}?${params}${airtableWhere}`;
        } else {
            url = `https://localhost:42123/${controller}/${endpoint}?${view}${airtableWhere}`;
            if (method == "DELETE") {
                url = `https://localhost:42123/${controller}/${endpoint}?id=${payload}`;
            } 
        }

        const response = await fetch(url, {
          method: method,
          headers: { "Content-Type": "application/json", Authorization: `Bearer ${token}` },
          body: payload ? JSON.stringify(payload) : null,
        });
        if (!response.ok) {
          throw new Error(`Failed to ${method} ${endpoint} ${JSON.stringify(response)}`);
        }
  
        return await response.json();
      } catch (ex) {
        console.error('Error: ', ex);
      }
    }
  }
  
  export default BaseService;
  

