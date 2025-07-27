import {
    AspectRatio, Center, HStack, Image, Textarea,
    Stack, FileUpload, Button, Input, defineStyle, Field, Box, NativeSelect,
    Text
} from "@chakra-ui/react";
import { HiUpload } from "react-icons/hi"
import { templates } from "../descriptionTemplates"
import { useActionData } from "react-router";
import { useState } from "react";

export default function ProductForm({ categories, defaultImageSrc = null, defaultName = "",
    defaultPrice = 0, defaultCategory = "CPU", defaultDescription = "", children }) {
    const actionData = useActionData();

    const [name, setName] = useState(defaultName
        ? defaultName
        : ""
    );
    const [price, setPrice] = useState(defaultPrice
        ? defaultPrice
        : 0
    );
    const [category, setCategory] = useState(defaultCategory
        ? defaultCategory
        : "CPU"
    );

    const [image, setImage] = useState({
        imageSrc: defaultImageSrc
    });

    const [description, setDescription] = useState(defaultDescription
        ? JSON.stringify(JSON.parse(defaultDescription), null, 2)
        : JSON.stringify(templates[category], null, 2)
    );

    const showPreview = e => {
        const imageFile = e.files[0];

        if (!imageFile) {
            return
        }

        const reader = new FileReader();
        reader.onload = () => {
            setImage({
                imageSrc: reader.result
            })
        }
        reader.readAsDataURL(imageFile);
    }

    return (
        <Center mt={20}>
            <Stack w={{ base: "95vw", sm: "90vw", md: "80vw", lg: "60vw" }}>
                <Text w="full" textAlign="center" textStyle="2xl" fontWeight="bold" mb={10}>{children}</Text>
                <HStack>

                    <Field.Root w="60%">
                        <Box pos="relative" w="100%">
                            <Input
                                type="text"
                                required
                                onInvalid={(e) => e.target.setCustomValidity("Пожалуйста, заполните это поле")}
                                onInput={(e) => {
                                    e.target.setCustomValidity("");
                                    setName(e.target.value)
                                }}
                                name="name"
                                variant="subtle"
                                maxLength={70}
                                className="peer"
                                placeholder=""
                                value={name}
                            />
                            <Field.Label css={floatingStyles}>Название продукта</Field.Label>
                        </Box>
                    </Field.Root>

                    <Field.Root w="20%">
                        <Box pos="relative" w="100%">
                            <Input
                                type="number"
                                min={0}
                                required
                                onInvalid={(e) => e.target.setCustomValidity("Пожалуйста, заполните это поле")}
                                onInput={(e) => {
                                    e.target.setCustomValidity("");
                                    setPrice(e.target.value);
                                }}
                                name="price"
                                variant="subtle"
                                maxLength={70}
                                className="peer"
                                placeholder=""
                                value={price}
                            />
                            <Field.Label css={floatingStyles}>Цена</Field.Label>
                        </Box>
                    </Field.Root>

                    <NativeSelect.Root w="20%">
                        <NativeSelect.Field
                            name="category"
                            value={category}
                            onChange={e => {
                                const currCategory = e.target.value;
                                setCategory(e.target.value);
                                setDescription(JSON.stringify(templates[currCategory], null, 2));
                            }}
                        >
                            {categories.map(category => (
                                <option key={category.id} value={category.category}>{category.category}</option>
                            ))}
                        </NativeSelect.Field>
                        <NativeSelect.Indicator />
                    </NativeSelect.Root>

                </HStack>
                <HStack align="stretch">
                    <Stack w="40%">

                        <AspectRatio ratio={1}>
                            <Image
                                borderWidth="1px"
                                borderStyle="solid"
                                borderColor={actionData?.errors?.image ? actionData?.imageBorderColor : "gray.300"}
                                bg="gray.100"
                                rounded="md"
                                src={image.imageSrc}>
                            </Image>
                        </AspectRatio>

                        <FileUpload.Root
                            accept={["image/png", "image/jpg", "image/jpeg"]}
                            name="image"
                            value={defaultImageSrc}
                            onFileAccept={showPreview}
                        >
                            <FileUpload.HiddenInput />
                            <FileUpload.Trigger asChild>
                                <Button variant="outline" size="sm" w="100%">
                                    <HiUpload /> Загрузить фотографию
                                </Button>
                            </FileUpload.Trigger>
                        </FileUpload.Root>

                    </Stack>

                    <Textarea
                        type="text"
                        required
                        onInvalid={(e) => e.target.setCustomValidity("Пожалуйста, заполните это поле")}
                        onInput={(e) => {
                            e.target.setCustomValidity("");
                            setDescription(e.target.value);
                        }}
                        flex="1"
                        resize="none"
                        variant="subtle"
                        name="description"
                        value={description}
                    />

                </HStack>
                <Button mt={6} size="md" type="submit">Добавить</Button>
            </Stack>
        </Center>
    );
}

const floatingStyles = defineStyle({
    pos: "absolute",
    bg: "gray.100",
    px: "0.5",
    top: "-3",
    insetStart: "2",
    fontWeight: "normal",
    pointerEvents: "none",
    transition: "position",
    _peerPlaceholderShown: {
        color: "fg.muted",
        top: "2.5",
        insetStart: "3",
    },
    _peerFocusVisible: {
        color: "fg",
        top: "-3",
        insetStart: "2",
        bg: "white",
    },
})