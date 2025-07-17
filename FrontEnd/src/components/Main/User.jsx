import { Avatar, Flex, Heading, LinkBox, LinkOverlay, Text } from '@chakra-ui/react';
import viteLogo from '/vite.svg';
import { Link, useLoaderData } from 'react-router';

export default function User() {
    const data = useLoaderData();

    return (
        <LinkBox gap={2} display="flex" alignItems="center">
            <Avatar.Root>
                <Avatar.Image src={viteLogo} size="sm" />
                <Avatar.Fallback name="Biba" />
            </Avatar.Root>
            <Heading>
                <LinkOverlay fontSize="sm" asChild>
                    <Link to="/login">
                        {data?.user?.name || "Вход / Регистрация"}
                    </Link>
                </LinkOverlay>
            </Heading>
        </LinkBox>
    )
}