// services/BaseService.js
class BaseService {
    async apiCall(method = "GET", controller = "User", endpoint = "AppUsers", view = "Grid%20view", payload = null, airtableWhere = "") {
      try {
        console.error('API CALL', method, controller, endpoint, view, payload, airtableWhere);
        const token = localStorage.getItem("access_token");
        if (!token && (endpoint !== "exchange" && controller !== "Guest")) {
          return null;
        }
  
        airtableWhere = airtableWhere ? `&airtableWhere=${airtableWhere}` : '';
        view = view ? `&view=${view}` : '';
  
        var url = `https://localhost:42123/${controller}/${endpoint}?${view}${airtableWhere}`;
        if (method == "DELETE") {
          url = `https://localhost:42123/${controller}/${endpoint}?id=${payload}`;
        } 

        const response = await fetch(url, {
          method: method,
          headers: { "Content-Type": "application/json", Authorization: `Bearer ${token}` },
          body: payload ? JSON.stringify(payload) : null,
        });

        console.error('RESPONSE', response);  
  
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
  

  
