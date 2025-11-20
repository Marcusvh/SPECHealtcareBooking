
const baseUrl = "https://localhost:7151/api";
const headers: HeadersInit = {
    "Content-Type": "application/json",
    "Accept": "application/json",
    // Optional CORS/Origin headers if needed (usually browser handles automatically)
    "Origin": "http://localhost:5173",
};

export async function apiRequest(
  endpoint: string,
  method: string = "GET",
  body?: any, 
  query? : string | ""
) {

  const options: RequestInit = {
    method,
    headers,
  };

  if (body) {
    options.body = JSON.stringify(body);
  }
  if(query) {
    query = "?" + query;
  }
  else {
    query = "";
  }

  try {
    const response = await fetch(`${baseUrl}/${endpoint}${query}`, options);

    if (!response.ok) {
      throw new Error(`API Error: ${response.status} ${response.statusText}`);
    }

    // Attempt to parse JSON if there is content
    const contentType = response.headers.get("content-type");
    if (contentType && contentType.includes("application/json")) {
      return await response.json();
    } else {
      return await response.text();
    }
  } catch (error) {
    console.error("API Request failed:", error);
    throw error;
  }
}