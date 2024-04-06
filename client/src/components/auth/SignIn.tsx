import { SubmitHandler, useForm } from "react-hook-form";

interface SignInFormKeys {
  email: string;
  password: string;
}

export default function SignIn() {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<SignInFormKeys>();

  const onSubmit: SubmitHandler<SignInFormKeys> = (data) => {
    console.log(data);
  };

  const inputClassName =
    "block w-full rounded-md border-0 py-1.5 pl-7 pr-20 text-gray-900 ring-1 ring-inset ring-gray-300 placeholder:text-gray-400 focus:ring-2 focus:ring-inset focus:ring-indigo-600 sm:text-sm sm:leading-6";

  const errorClassName = "text-red-500";

  return (
    <div className="flex justify-center items-center h-full">
      <div className="flex flex-col justify-center gap-5 rounded-lg shadow-lg p-4 w-96 h-96">
        <h2 className="self-center text-xl font-bold">Sign In</h2>
        <form
          className="flex flex-col items-center gap-5"
          onSubmit={handleSubmit(onSubmit)}
        >
          <div>
            <label className="block text-sm font-medium leading-6 text-gray-900">
              Email
            </label>
            <div className="relative mt-2 rounded-md shadow-sm">
              <input
                className={inputClassName}
                {...register("email", { required: "Email is required" })}
              />
            </div>
            <p className={errorClassName}>{errors.email?.message}</p>
          </div>

          <div>
            <label className="block text-sm font-medium leading-6 text-gray-900">
              Password
            </label>
            <div className="relative mt-2 rounded-md shadow-sm">
              <input
                type="password"
                className={inputClassName}
                {...register("password", {
                  required: "Password is required",
                  minLength: {
                    value: 8,
                    message: "Minimal password length is 8",
                  },
                })}
              />
            </div>
            <p className={errorClassName}>{errors.password?.message}</p>
          </div>
          <button
            type="submit"
            className="w-40 font-semibold text-white bg-sky-500 hover:bg-sky-400 rounded-3xl py-2 px-4"
          >
            Send
          </button>
        </form>
      </div>
    </div>
  );
}
