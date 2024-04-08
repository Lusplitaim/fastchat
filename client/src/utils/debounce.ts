export default function debounce(callback: TimerHandler, delay: number) {
  let timeout: number;
  return function () {
    clearTimeout(timeout);
    timeout = setTimeout(callback, delay);
  };
}
