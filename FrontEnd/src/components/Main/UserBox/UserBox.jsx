import { Heading, LinkBox, LinkOverlay } from '@chakra-ui/react';
import { Link } from 'react-router';
import Avatar from './Avatar';
import viteLogo from '/vite.svg';

export default function UserBox({ children, to = "/", src = viteLogo, fallbackName = "User" }) {
    return (
        <LinkBox gap={2} display="flex" alignItems="center">
            <Avatar src={src} fallbackName={fallbackName} />
            <Heading>
                <LinkOverlay as={Link} to={to} fontSize="sm">
                    {children}
                </LinkOverlay>
            </Heading>
        </LinkBox>
    );
}