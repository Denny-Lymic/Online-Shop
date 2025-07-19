import viteLogo from '/vite.svg';
import { Await, useLoaderData } from 'react-router';
import { Suspense } from 'react';
import UserBox from './UserBox';

export default function User() {
    const { user } = useLoaderData();

    return (
        <Suspense fallback={
            <UserBox src={viteLogo}>
                Загрузка...
            </UserBox>
        }>
            <Await resolve={user}>
                {(data) => (
                    <UserBox to={data?.user ? "/" : "/login"}>
                        {/* `/profile/${data.user.id}` */}
                        {data?.user?.name || "Вход/Регистрация"}
                    </UserBox>
                )}
            </Await>
        </Suspense>
    )
}