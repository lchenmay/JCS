export const sleep = (ms: number) => {
  return new Promise(resolve => setTimeout(resolve, ms));
}

export const url__Params = (url: string): Record<string, string> => {
  const searchParams = new URLSearchParams(url.split('?')[1]);
  const params: Record<string, string> = {};
  
  for (const [key, value] of searchParams.entries()) {
    params[key] = value;
  }
  
  return params;
}