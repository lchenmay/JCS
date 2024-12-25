export const is_local = () => {
  return ["127.0.0.1", "localhost"].includes(window.location.hostname)
}