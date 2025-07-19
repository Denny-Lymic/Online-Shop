import { Heading, LinkBox, LinkOverlay } from '@chakra-ui/react';
import viteLogo from '/vite.svg';
import { Await, data, Link, useLoaderData } from 'react-router';
import { Suspense, useEffect } from 'react';
import UserBox from './UserBox/UserBox';

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